using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using WaracleHotelBooking.Controllers;
using WaracleHotelBooking.DTOs;
using WaracleHotelBooking.Models;
using WaracleHotelBooking.Services;
using WaracleHotelBooking.Tests.Utils;

namespace WaracleHotelBooking.Tests.Controllers
{
    [ExcludeFromCodeCoverage]
    public class BookingControllerTests
    {
        [Fact]
        public async Task CreateBooking_ReturnsBadRequest_WhenEndDateBeforeStart()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new BookingController(db, new BookingService(db));

            var request = new CreateBookingRequest(
                RoomId: db.Rooms.First().Id,
                StartDate: new DateTime(2025, 12, 10),
                EndDate: new DateTime(2025, 12, 9),
                Guests: 1
            );

            var result = await controller.CreateBooking(request);

            result.Should().BeOfType<BadRequestObjectResult>();
        }


        [Fact]
        public async Task CreateBooking_ReturnsBadRequest_WhenRoomNotAvailable()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new BookingController(db, new BookingService(db));

            var request = new CreateBookingRequest(
                RoomId: db.Rooms.First().Id,
                StartDate: new DateTime(2025, 12, 3),
                EndDate: new DateTime(2025, 12, 14),
                Guests: 1
            );

            var result = await controller.CreateBooking(request);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CreateBooking_ReturnsOk_WhenRoomAvailable()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new BookingController(db, new BookingService(db));

            var request = new CreateBookingRequest(
                RoomId: db.Rooms.First().Id,
                StartDate: new DateTime(2025, 12, 12),
                EndDate: new DateTime(2025, 12, 14),
                Guests: 1
            );

            var result = await controller.CreateBooking(request);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task FindAvailableRooms_ReturnsBadResult_WhenEndDateBeforeStart()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new BookingController(db, new BookingService(db));

            var request = new FindRoomRequest(
                HotelId: db.Hotels.First().Id,
                RoomType: null,
                StartDate: new DateTime(2025, 12, 12),
                EndDate: new DateTime(2025, 12, 11),
                Guests: 1
            );

            var result = await controller.FindAvailableRooms(request);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task FindAvailableRooms_ReturnsOk_WhenRoomsAvailable()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new BookingController(db, new BookingService(db));

            var request = new FindRoomRequest(
                HotelId: db.Hotels.First().Id,
                RoomType: null,
                StartDate: new DateTime(2025, 12, 12),
                EndDate: new DateTime(2025, 12, 14),
                Guests: 1
            );

            var result = await controller.FindAvailableRooms(request);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task FindAvailableRooms_ReturnsOk_WhenTotalRoomsAvailable()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new BookingController(db, new BookingService(db));

            var request = new FindRoomRequest(
                HotelId: db.Hotels.First().Id,
                RoomType: null,
                StartDate: new DateTime(2025, 12, 5),
                EndDate: new DateTime(2025, 12, 6),
                Guests: 5
            );

            var result = await controller.FindAvailableRooms(request);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task FindAvailableRooms_ReturnsBadRequest_WhenTotalRoomsNotAvailable()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new BookingController(db, new BookingService(db));

            var request = new FindRoomRequest(
                HotelId: db.Hotels.First().Id,
                RoomType: null,
                StartDate: new DateTime(2025, 12, 5),
                EndDate: new DateTime(2025, 12, 6),
                Guests: 6
            );

            var result = await controller.FindAvailableRooms(request);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task FindAvailableRooms_ReturnsOk_WhenRoomsOfType0Available()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new BookingController(db, new BookingService(db));

            var request = new FindRoomRequest(
                HotelId: db.Hotels.First().Id,
                RoomType: RoomType.Deluxe,
                StartDate: new DateTime(2025, 12, 12),
                EndDate: new DateTime(2025, 12, 14),
                Guests: 4
            );

            var result = await controller.FindAvailableRooms(request);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task FindAvailableRooms_ReturnsOk_WhenRoomsOfType1Available()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new BookingController(db, new BookingService(db));

            var request = new FindRoomRequest(
                HotelId: db.Hotels.First().Id,
                RoomType: RoomType.Single,
                StartDate: new DateTime(2025, 12, 12),
                EndDate: new DateTime(2025, 12, 14),
                Guests: 1
            );

            var result = await controller.FindAvailableRooms(request);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task FindAvailableRooms_ReturnsOk_WhenRoomsOfType2Available()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new BookingController(db, new BookingService(db));

            var request = new FindRoomRequest(
                HotelId: db.Hotels.First().Id,
                RoomType: RoomType.Double,
                StartDate: new DateTime(2025, 12, 12),
                EndDate: new DateTime(2025, 12, 14),
                Guests: 2
            );

            var result = await controller.FindAvailableRooms(request);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task FindAvailableRooms_ReturnsBadRequest_WhenRoomsNotAvailable()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new BookingController(db, new BookingService(db));

            var request = new FindRoomRequest(
                HotelId: db.Hotels.First().Id,
                RoomType: null,
                StartDate: new DateTime(2025, 12, 5),
                EndDate: new DateTime(2025, 12, 6),
                Guests: 10
            );

            var result = await controller.FindAvailableRooms(request);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task FindAvailableRooms_ReturnsBadRequest_WhenRoomsOfType0NotAvailable()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new BookingController(db, new BookingService(db));

            var request = new FindRoomRequest(
                HotelId: db.Hotels.First().Id,
                RoomType: RoomType.Deluxe,
                StartDate: new DateTime(2025, 12, 5),
                EndDate: new DateTime(2025, 12, 6),
                Guests: 4
            );

            var result = await controller.FindAvailableRooms(request);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task FindAvailableRooms_ReturnsBadRequest_WhenRoomsOfType1NotAvailable()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new BookingController(db, new BookingService(db));

            var request = new FindRoomRequest(
                HotelId: db.Hotels.First().Id,
                RoomType: RoomType.Single,
                StartDate: new DateTime(2025, 12, 7),
                EndDate: new DateTime(2025, 12, 9),
                Guests: 1
            );

            var result = await controller.FindAvailableRooms(request);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task FindAvailableRooms_ReturnsBadRequest_WhenRoomsOfType2NotAvailable()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new BookingController(db, new BookingService(db));

            var request = new FindRoomRequest(
                HotelId: db.Hotels.First().Id,
                RoomType: RoomType.Double,
                StartDate: new DateTime(2025, 12, 7),
                EndDate: new DateTime(2025, 12, 10),
                Guests: 4
            );

            var result = await controller.FindAvailableRooms(request);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetBooking_ReturnsOk_WhenBookingFound()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new BookingController(db, new BookingService(db));

            var booking = db.Bookings.First();

            var result = await controller.GetBooking(booking.BookingReference);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetBooking_ReturnsNotFound_WhenBookingNotFound()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new BookingController(db, new BookingService(db));

            var booking = db.Bookings.First();

            var result = await controller.GetBooking("bookingReference");

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}