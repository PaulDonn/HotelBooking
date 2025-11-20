using System.Diagnostics.CodeAnalysis;

namespace WaracleHotelBooking.Models
{
    [ExcludeFromCodeCoverage]
    public class Booking
    {
        public Guid Id { get; set; }
        public string BookingReference { get; set; } = string.Empty;
        public Guid RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Guests { get; set; }
    }
}
