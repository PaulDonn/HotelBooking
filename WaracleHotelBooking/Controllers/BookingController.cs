using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaracleHotelBooking.DataModel.Data;
using WaracleHotelBooking.DTOs;
using WaracleHotelBooking.DataModel.Models;
using WaracleHotelBooking.Services;
using Azure;

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

        /// <summary>
        /// Creates a booking record
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest req)
        {
            if (req.EndDate.Date <= req.StartDate.Date)
                return BadRequest("End date must be after start date.");

            if (req.StartDate.Date < DateTime.Now.Date)
                return BadRequest("Bookings may not be created before today's date.");

            var roomAvailability = await _svc.CheckRoomAvailability(req.RoomId, req.StartDate.Date, req.EndDate.Date, req.Guests);
            if (!roomAvailability)
            {
                return BadRequest("Room not available.");
            }

            var booking = await _svc.CreateBooking(req.RoomId, req.StartDate.Date, req.EndDate.Date, req.Guests);
            return Ok(booking);
        }


        /// <summary>
        /// Finds all rooms with availability between set dates and returns them ordered by suitability
        /// </summary>
        /// <returns>A list of rooms.</returns>
        [HttpPost("FindAvailableRooms")]
        public async Task<IActionResult> FindAvailableRooms([FromBody] FindRoomRequest req)
        {
            if (req.EndDate.Date <= req.StartDate.Date)
                return BadRequest("End date must be after start date.");

            if (req.StartDate.Date < DateTime.Now.Date)
                return BadRequest("Bookings may not be created before today's date.");


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


        /// <summary>
        /// Gets a booking record by Booking Reference
        /// </summary>
        /// <returns>A booking record.</returns>
        [HttpGet("{reference}")]
        public async Task<IActionResult> GetBooking(string reference)
        {
            var booking = await _db.Bookings.FirstOrDefaultAsync(b => b.BookingReference == reference);
            return booking == null ? NotFound() : Ok(booking);
        }


        /// <summary>
        /// Deletes a booking record by Booking reference
        /// </summary>
        [HttpDelete("{reference}")]
        public async Task<IActionResult> DeleteBooking(string reference)
        {
            var booking = await _db.Bookings.FirstOrDefaultAsync(b => b.BookingReference == reference);

            if(booking == null)
            {
                return NotFound();
            }

            if(booking.StartDate >= DateTime.Now.Date)
            {
                return BadRequest($"Bookings may not be deleted after their start date.");
            }

            if(!await _svc.DeleteBooking(booking))
            {
                return BadRequest($"Booking {reference} could not be deleted.");
            }

            return Ok();
        }
    }
}
