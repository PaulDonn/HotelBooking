using System.Diagnostics.CodeAnalysis;
using WaracleHotelBooking.Models;

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
