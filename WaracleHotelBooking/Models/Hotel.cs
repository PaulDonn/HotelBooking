using System.Diagnostics.CodeAnalysis;

namespace WaracleHotelBooking.Models
{
    [ExcludeFromCodeCoverage]
    public class Hotel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Room> Rooms { get; set; } = new();
        public string BookingRefPrefix = string.Empty;
    }
}