using System.Diagnostics.CodeAnalysis;
using WaracleHotelBooking.DataModel.Models;

namespace WaracleHotelBooking.DTOs
{
    [ExcludeFromCodeCoverage]
    public record CreateBookingRequest(
    Guid RoomId,
    DateTime StartDate,
    DateTime EndDate,
    int Guests
    );
}
