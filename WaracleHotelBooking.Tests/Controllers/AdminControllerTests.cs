using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using WaracleHotelBooking.Controllers;
using WaracleHotelBooking.Tests.Utils;

namespace WaracleHotelBooking.Tests.Controllers
{
    [ExcludeFromCodeCoverage]
    public class AdminControllerTests
    {
        [Fact]
        public void Reset_ClearsAllData()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new AdminController(db);

            controller.Reset();

            db.Hotels.Should().BeEmpty();
            db.Rooms.Should().BeEmpty();
            db.Bookings.Should().BeEmpty();
        }

        [Fact]
        public void Seed_DoesNotRepopulateDataWhenNotEmpty()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new AdminController(db);

            var hotelCount = db.Hotels.Count();
            var hotel1 = db.Hotels.First().Id;

            controller.Seed();

            db.Hotels.Should().NotBeEmpty();
            db.Rooms.Should().NotBeEmpty();
            db.Bookings.Should().NotBeEmpty();

            db.Hotels.Count().Should().Be(hotelCount);
            db.Hotels.First().Id.Should().Be(hotel1);

        }

        [Fact]
        public void Seed_RepopulatesData()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new AdminController(db);

            controller.Reset();
            controller.Seed();

            db.Hotels.Should().NotBeEmpty();
            db.Rooms.Should().NotBeEmpty();
            db.Bookings.Should().NotBeEmpty();
        }

        [Fact]
        public void Seed_RepopulatesHotel1Bookings()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new AdminController(db);

            controller.Reset();
            controller.Seed();

            var hotel = db.Hotels.Include(h => h.Rooms).SingleOrDefault(x => x.Name == "Pinnacle View Suites");
            hotel.Should().NotBeNull();

            var h1r1Guid = new Guid("1b373ddb-8116-4252-afc5-17bd6dc9a331");
            var h1r2Guid = new Guid("d83cb86a-288b-4640-a4c7-1fdff69cbf02");
            var h1r3Guid = new Guid("1d05a42e-2fbf-4dee-bad3-8fb6eb368e85");
            var h1r4Guid = new Guid("f91c5818-3727-482c-a039-299c9902fdd1");
            var h1r5Guid = new Guid("bbb1557d-0b85-43c6-80fd-da70fc811186");
            var h1r6Guid = new Guid("e382d73e-1223-4d98-acbb-a99a327c146a");

            hotel.Rooms.Should().ContainSingle(r => r.Id == h1r1Guid);
            hotel.Rooms.Should().ContainSingle(r => r.Id == h1r2Guid);
            hotel.Rooms.Should().ContainSingle(r => r.Id == h1r3Guid);
            hotel.Rooms.Should().ContainSingle(r => r.Id == h1r4Guid);
            hotel.Rooms.Should().ContainSingle(r => r.Id == h1r5Guid);
            hotel.Rooms.Should().ContainSingle(r => r.Id == h1r6Guid);

            hotel.Rooms.Count().Should().Be(6);

            var bookings = db.Bookings.ToList();
            bookings.Should().ContainSingle(b => b.RoomId == h1r1Guid
                                                 && b.StartDate == new DateTime(2025, 12, 2)
                                                 && b.EndDate == new DateTime(2025, 12, 4)
                                                 && b.Guests == 1);

            bookings.Should().ContainSingle(b => b.RoomId == h1r1Guid
                                                 && b.StartDate == new DateTime(2025, 12, 4)
                                                 && b.EndDate == new DateTime(2025, 12, 8)
                                                 && b.Guests == 1);

            bookings.Should().ContainSingle(b => b.RoomId == h1r1Guid
                                                 && b.StartDate == new DateTime(2025, 12, 9)
                                                 && b.EndDate == new DateTime(2025, 12, 11)
                                                 && b.Guests == 1);

            bookings.Should().ContainSingle(b => b.RoomId == h1r2Guid
                                                 && b.StartDate == new DateTime(2025, 12, 1)
                                                 && b.EndDate == new DateTime(2025, 12, 2)
                                                 && b.Guests == 1);

            bookings.Should().ContainSingle(b => b.RoomId == h1r2Guid
                                                 && b.StartDate == new DateTime(2025, 12, 8)
                                                 && b.EndDate == new DateTime(2025, 12, 10)
                                                 && b.Guests == 1);

            bookings.Should().ContainSingle(b => b.RoomId == h1r3Guid
                                                 && b.StartDate == new DateTime(2025, 12, 1)
                                                 && b.EndDate == new DateTime(2025, 12, 3)
                                                 && b.Guests == 1);

            bookings.Should().ContainSingle(b => b.RoomId == h1r3Guid
                                                 && b.StartDate == new DateTime(2025, 12, 9)
                                                 && b.EndDate == new DateTime(2025, 12, 12)
                                                 && b.Guests == 2);

            bookings.Should().NotContain(b => b.RoomId == h1r4Guid);


            bookings.Should().ContainSingle(b => b.RoomId == h1r5Guid
                                                 && b.StartDate == new DateTime(2025, 12, 1)
                                                 && b.EndDate == new DateTime(2025, 12, 5)
                                                 && b.Guests == 4);

            bookings.Should().ContainSingle(b => b.RoomId == h1r5Guid
                                                 && b.StartDate == new DateTime(2025, 12, 5)
                                                 && b.EndDate == new DateTime(2025, 12, 9)
                                                 && b.Guests == 3);

            bookings.Should().ContainSingle(b => b.RoomId == h1r5Guid
                                                 && b.StartDate == new DateTime(2025, 12, 9)
                                                 && b.EndDate == new DateTime(2025, 12, 12)
                                                 && b.Guests == 4);



        }
    }
}