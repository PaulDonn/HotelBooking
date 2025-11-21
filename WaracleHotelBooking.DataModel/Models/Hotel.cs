using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WaracleHotelBooking.DataModel.Models
{
    [ExcludeFromCodeCoverage]
    public class Hotel
    {
        public Guid Id { get; set; }
        
        public required string Name { get; set; }
        public required string BookingRefPrefix { get; set; }
        public List<Room> Rooms { get; set; } = new();
    }
}