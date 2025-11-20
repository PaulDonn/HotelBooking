using Microsoft.EntityFrameworkCore;
using WaracleHotelBooking.DataModel.Data;
using WaracleHotelBooking.DataModel.Models;

namespace WaracleHotelBooking.Services
{
    public class BookingService
    {
        private readonly AppDbContext _db;
        public BookingService(AppDbContext db) => _db = db;


        public async Task<List<Room>> GetAvailableRooms(Guid hotelId, RoomType? type, DateTime start, DateTime end, int guests)
        {
            return await _db.Rooms
            .Where(r => r.HotelId == hotelId && (type == null || r.RoomType == type)) //the number of guests is not important at this time, we are returning all available rooms.
            .Where(r => !_db.Bookings.Any(b => b.RoomId == r.Id && start < b.EndDate && end > b.StartDate)) //a guest may check in on the same day another guest checks out.
            .ToListAsync();
        }

        public async Task<bool> CheckRoomAvailability(Guid roomId, DateTime start, DateTime end, int guests)
        {
            var room = await _db.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
            var existingBooking = await _db.Bookings.AnyAsync(b => b.RoomId == roomId && start <= b.EndDate && end >= b.StartDate); //a guest may check in on the same day another guest checks out.

            if (room != null && room.Capacity >= guests && !existingBooking)
            {
                return true;
            }
            return false;
        }

        public async Task<Booking> CreateBooking(Guid roomId, DateTime start, DateTime end, int guests)
        {
            var booking = new Booking
            {
                RoomId = roomId,
                StartDate = start,
                EndDate = end,
                Guests = guests,
                BookingReference = Guid.NewGuid().ToString()[..8].ToUpper(),
            };


            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync();
            return booking;
        }
    }
}
