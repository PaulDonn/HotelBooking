using System.Diagnostics.CodeAnalysis;
using WaracleHotelBooking.DataModel.Models;

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
