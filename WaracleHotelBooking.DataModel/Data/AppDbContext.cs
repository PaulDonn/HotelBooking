using Microsoft.EntityFrameworkCore;
using WaracleHotelBooking.DataModel.Models;

namespace WaracleHotelBooking.DataModel.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Hotel> Hotels => Set<Hotel>();
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Booking> Bookings => Set<Booking>();
    }
}