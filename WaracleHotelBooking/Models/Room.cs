using System.Diagnostics.CodeAnalysis;

namespace WaracleHotelBooking.Models
{
    [ExcludeFromCodeCoverage]
    public class Room
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public RoomType RoomType { get; set; }
        public int Capacity { get; set; }
    }
}