using WaracleHotelBooking.Services;
using WaracleHotelBooking.Tests.Utils;
using FluentAssertions;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace WaracleHotelBooking.Tests.Services
{
    [ExcludeFromCodeCoverage]
    public class BookingServiceTests
    {


        [Fact]
        public async Task GetAvailableRooms_ReturnsRooms_WhenAvailable()
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
        public async Task GetAvailableRooms_ReturnsNoRooms_WhenNotAvailable()
        {
            var db = TestDbContextFactory.CreateContext();
            var service = new BookingService(db);

            var hotel = db.Hotels.Include(hotel => hotel.Rooms).First();

            foreach (var room in hotel.Rooms)
            {
                await service.CreateBooking(room.Id, new DateTime(2025, 11, 01), new DateTime(2025, 11, 12), 1);
            }

            var start = new DateTime(2025, 11, 05);
            var end = new DateTime(2025, 11, 06);

            var rooms = await service.GetAvailableRooms(hotel.Id, null, start, end, 1);

            rooms.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAvailableRooms_ReturnsNoRooms_WhenNotAvailableSameDate()
        {
            var db = TestDbContextFactory.CreateContext();
            var service = new BookingService(db);

            var hotel = db.Hotels.Include(hotel => hotel.Rooms).First();

            foreach (var room in hotel.Rooms)
            {
                await service.CreateBooking(room.Id, new DateTime(2025, 11, 01), new DateTime(2025, 11, 12), 1);
            }

            var start = new DateTime(2025, 11, 01);
            var end = new DateTime(2025, 11, 12);

            var rooms = await service.GetAvailableRooms(hotel.Id, null, start, end, 1);

            rooms.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAvailableRooms_ReturnsNoRooms_WhenNotAvailableStartEdge()
        {
            var db = TestDbContextFactory.CreateContext();
            var service = new BookingService(db);

            var hotel = db.Hotels.Include(hotel => hotel.Rooms).First();

            foreach (var room in hotel.Rooms)
            {
                await service.CreateBooking(room.Id, new DateTime(2025, 11, 01), new DateTime(2025, 11, 12), 1);
            }

            var start = new DateTime(2025, 11, 01);
            var end = new DateTime(2025, 11, 02);

            var rooms = await service.GetAvailableRooms(hotel.Id, null, start, end, 1);

            rooms.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAvailableRooms_ReturnsNoRooms_WhenNotAvailableEndEdge()
        {
            var db = TestDbContextFactory.CreateContext();
            var service = new BookingService(db);

            var hotel = db.Hotels.Include(hotel => hotel.Rooms).First();

            foreach (var room in hotel.Rooms)
            {
                await service.CreateBooking(room.Id, new DateTime(2025, 11, 01), new DateTime(2025, 11, 12), 1);
            }

            var start = new DateTime(2025, 11, 05);
            var end = new DateTime(2025, 11, 12);

            var rooms = await service.GetAvailableRooms(hotel.Id, null, start, end, 1);

            rooms.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAvailableRooms_ReturnsRooms_WhenAvailableAtStartEdge()
        {
            var db = TestDbContextFactory.CreateContext();
            var service = new BookingService(db);

            var hotel = db.Hotels.Include(hotel => hotel.Rooms).First();

            foreach (var room in hotel.Rooms)
            {
                await service.CreateBooking(room.Id, new DateTime(2025, 11, 01), new DateTime(2025, 11, 12), 1);
            }

            var start = new DateTime(2025, 10, 27);
            var end = new DateTime(2025, 11, 01);

            var rooms = await service.GetAvailableRooms(hotel.Id, null, start, end, 1);

            rooms.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetAvailableRooms_ReturnsRooms_WhenAvailableAtEndEdge()
        {
            var db = TestDbContextFactory.CreateContext();
            var service = new BookingService(db);

            var hotel = db.Hotels.Include(hotel => hotel.Rooms).First();

            foreach (var room in hotel.Rooms)
            {
                await service.CreateBooking(room.Id, new DateTime(2025, 11, 01), new DateTime(2025, 11, 12), 1);
            }

            var start = new DateTime(2025, 11, 12);
            var end = new DateTime(2025, 11, 15);

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

        [Fact]
        public async Task CreateBooking_ReturnsBooking_WhenValid()
        {
            var db = TestDbContextFactory.CreateContext();
            var service = new BookingService(db);

            var room = db.Rooms.First();
            var start = new DateTime(2025, 12, 12);
            var end = new DateTime(2025, 12, 14);

            var result = await service.CreateBooking(room.Id, start, end, 1);

            result.Should().NotBeNull();

            var hotel = db.Hotels.First(h => h.Id == room.HotelId);
            result.BookingReference.Should().StartWith(hotel.BookingRefPrefix);
            result.StartDate.Should().Be(start);
            result.EndDate.Should().Be(end);
            result.Guests.Should().Be(1);
        }
    }
}