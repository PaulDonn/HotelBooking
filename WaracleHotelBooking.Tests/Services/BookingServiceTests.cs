using WaracleHotelBooking.Services;
using WaracleHotelBooking.Tests.Utils;
using FluentAssertions;
using System.Diagnostics.CodeAnalysis;

namespace WaracleHotelBooking.Tests.Services
{
    [ExcludeFromCodeCoverage]
    public class BookingServiceTests
    {


        [Fact]
        public async Task GetAvailableRooms_ReturnsRooms_WhenNotOverlapping()
        {
            var db = TestDbContextFactory.CreateContext();
            var service = new BookingService(db);

            var hotel = db.Hotels.First();
            var start = new DateTime(2025, 12, 12);
            var end = new DateTime(2025, 12, 14);

            var rooms = await service.GetAvailableRooms(hotel.Id, null, start, end, 1);

            rooms.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CheckRoomAvailability_ReturnsFalse_WhenOverlapping()
        {
            var db = TestDbContextFactory.CreateContext();
            var service = new BookingService(db);

            var existing = db.Bookings.First();
            var result = await service.CheckRoomAvailability(
                existing.RoomId,
                existing.StartDate,
                existing.EndDate,
                existing.Guests);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task CheckRoomAvailability_ReturnsFalse_WhenGuestsExceedsCapacity()
        {
            var db = TestDbContextFactory.CreateContext();
            var service = new BookingService(db);

            var room = db.Rooms.First();
            var start = new DateTime(2025, 12, 12);
            var end = new DateTime(2025, 12, 14);

            var result = await service.CheckRoomAvailability(room.Id, start, end, 10);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task CheckRoomAvailability_ReturnsTrue_WhenGuestsEqualsCapacity()
        {
            var db = TestDbContextFactory.CreateContext();
            var service = new BookingService(db);

            var room = db.Rooms.First();
            var start = new DateTime(2025, 12, 12);
            var end = new DateTime(2025, 12, 14);

            var result = await service.CheckRoomAvailability(room.Id, start, end, room.Capacity);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task CheckRoomAvailability_ReturnsTrue_WhenGuestsIsLessThanCapacity()
        {
            var db = TestDbContextFactory.CreateContext();
            var service = new BookingService(db);

            var room = db.Rooms.First(r => r.Capacity > 1);
            var start = new DateTime(2025, 12, 05);
            var end = new DateTime(2025, 12, 08);

            var result = await service.CheckRoomAvailability(room.Id, start, end, 1);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task CheckRoomAvailability_ReturnsTrue_WhenNoOverlap()
        {
            var db = TestDbContextFactory.CreateContext();
            var service = new BookingService(db);

            var room = db.Rooms.First();
            var start = new DateTime(2025, 12, 12);
            var end = new DateTime(2025, 12, 14);

            var result = await service.CheckRoomAvailability(room.Id, start, end, 1);

            result.Should().BeTrue();
        }
    }
}