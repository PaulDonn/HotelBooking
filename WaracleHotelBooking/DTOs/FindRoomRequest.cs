using System.Diagnostics.CodeAnalysis;
using WaracleHotelBooking.Models;

namespace WaracleHotelBooking.DTOs
{
    [ExcludeFromCodeCoverage]
    public record FindRoomRequest(
    Guid HotelId,
    RoomType? RoomType,
    DateTime StartDate,
    DateTime EndDate,
    int Guests
    );
}
