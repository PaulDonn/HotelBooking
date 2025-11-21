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
            var hotel = await _db.Hotels.SingleAsync(h => h.Rooms.Any(r => r.Id == roomId));
            var uniqueRef = string.Empty;
            while(string.IsNullOrEmpty(uniqueRef))
            {
                var bookingRef = hotel.BookingRefPrefix + Guid.NewGuid().ToString()[..8].ToUpper(); //collision possible with substring of guid as booking ref
                if(!_db.Bookings.Any(b => b.BookingReference == bookingRef))
                {
                    uniqueRef = bookingRef;
                }
            }

            var booking = new Booking
            {
                RoomId = roomId,
                StartDate = start,
                EndDate = end,
                Guests = guests,
                BookingReference = uniqueRef,
            };

            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync();
            return booking;
        }

        public async Task<bool> DeleteBooking(Booking booking)
        {
            try
            {
                _db.Bookings.Remove(booking);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
