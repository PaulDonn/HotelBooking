using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaracleHotelBooking.DataModel.Data;
using WaracleHotelBooking.DTOs;
using WaracleHotelBooking.DataModel.Models;
using WaracleHotelBooking.Services;

namespace WaracleHotelBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly BookingService _svc;


        public BookingController(AppDbContext db, BookingService svc)
        {
            _db = db;
            _svc = svc;
        }


        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest req)
        {
            if (req.EndDate.Date <= req.StartDate.Date)
                return BadRequest("End date must be after start date.");

            var roomAvailability = await _svc.CheckRoomAvailability(req.RoomId, req.StartDate.Date, req.EndDate.Date, req.Guests);
            if (!roomAvailability)
            {
                return BadRequest("Room not available.");
            }

            var booking = await _svc.CreateBooking(req.RoomId, req.StartDate.Date, req.EndDate.Date, req.Guests);
            return Ok(booking);
        }

        [HttpPost("FindAvailableRooms")]
        public async Task<IActionResult> FindAvailableRooms([FromBody] FindRoomRequest req)
        {
            if (req.EndDate.Date <= req.StartDate.Date)
                return BadRequest("End date must be after start date.");


            var rooms = await _svc.GetAvailableRooms(req.HotelId, req.RoomType, req.StartDate.Date, req.EndDate.Date, req.Guests);
            var maxCapacity = rooms.Sum(r => r.Capacity);
            if (rooms == null || !rooms.Any() || maxCapacity < req.Guests) //No rooms available to meet the specifications
                return BadRequest("No rooms available.");

            //Order rooms by suitability
            var orderedRooms = new List<Room>();
            foreach(var room in rooms.Where(r => r.Capacity == req.Guests)) //prioritise rooms where the capacity is exact for the number of guests
            {
                orderedRooms.Add(room);
            }
            foreach(var room in rooms.Where(r => r.Capacity > req.Guests)) //then list larger capacity rooms than the number of guests
            {
                orderedRooms.Add(room);
            }
            foreach(var room in rooms.Where(r => r.Capacity < req.Guests).OrderByDescending(r => r.Capacity)) //follow up with smaller capacity rooms so multiple bookings may be made
            {
                orderedRooms.Add(room);
            }

            return Ok(orderedRooms);
        }


        [HttpGet("{reference}")]
        public async Task<IActionResult> GetBooking(string reference)
        {
            var booking = await _db.Bookings.FirstOrDefaultAsync(b => b.BookingReference == reference);
            return booking is null ? NotFound() : Ok(booking);
        }
    }
}
