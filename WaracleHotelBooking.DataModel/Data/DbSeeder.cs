using System;
using WaracleHotelBooking.DataModel.Models;

namespace WaracleHotelBooking.DataModel.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            if (db.Hotels.Any()) return;

            //Hotel 1 - Siena Suites - 2 Singles, 2 Doubles, 2 Deluxe

            var hotel1 = new Hotel { Name = "Pinnacle View Suites", BookingRefPrefix = "PVS" };
            db.Hotels.Add(hotel1);
            db.SaveChanges();

            //populate specific guids for tests
            var h1r1Guid = new Guid("1b373ddb-8116-4252-afc5-17bd6dc9a331");
            var h1r2Guid = new Guid("d83cb86a-288b-4640-a4c7-1fdff69cbf02");
            var h1r3Guid = new Guid("1d05a42e-2fbf-4dee-bad3-8fb6eb368e85");
            var h1r4Guid = new Guid("f91c5818-3727-482c-a039-299c9902fdd1");
            var h1r5Guid = new Guid("bbb1557d-0b85-43c6-80fd-da70fc811186");
            var h1r6Guid = new Guid("e382d73e-1223-4d98-acbb-a99a327c146a");

            var rooms1 = new List<Room>
            {
                new Room { Id= h1r1Guid, HotelId = hotel1.Id, Name = "101", RoomType = RoomType.Single, Capacity = 1 },
                new Room { Id= h1r2Guid, HotelId = hotel1.Id, Name = "102", RoomType = RoomType.Single, Capacity = 1 },
                new Room { Id= h1r3Guid, HotelId = hotel1.Id, Name = "201", RoomType = RoomType.Double, Capacity = 2 },
                new Room { Id= h1r4Guid, HotelId = hotel1.Id, Name = "202", RoomType = RoomType.Double, Capacity = 2 },
                new Room { Id= h1r5Guid, HotelId = hotel1.Id, Name = "301", RoomType = RoomType.Deluxe, Capacity = 4 },
                new Room { Id= h1r6Guid, HotelId = hotel1.Id, Name = "302", RoomType = RoomType.Deluxe, Capacity = 4 },
            };


            db.Rooms.AddRange(rooms1);
            db.SaveChanges();

            var bookings1 = new List<Booking>()
            {
                new Booking{ RoomId = h1r1Guid, Guests = 1, StartDate = new DateTime(2025,12,2), EndDate = new DateTime(2025,12,4), BookingReference = hotel1.BookingRefPrefix + Guid.NewGuid().ToString()[0..8].ToUpper()},
                new Booking{ RoomId = h1r1Guid, Guests = 1, StartDate = new DateTime(2025,12,4), EndDate = new DateTime(2025,12,8), BookingReference = hotel1.BookingRefPrefix + Guid.NewGuid().ToString()[0..8].ToUpper()},
                new Booking{ RoomId = h1r1Guid, Guests = 1, StartDate = new DateTime(2025,12,9), EndDate = new DateTime(2025,12,11), BookingReference = hotel1.BookingRefPrefix + Guid.NewGuid().ToString()[0..8].ToUpper()},

                new Booking{ RoomId = h1r2Guid, Guests = 1, StartDate = new DateTime(2025,12,1), EndDate = new DateTime(2025,12,2), BookingReference = hotel1.BookingRefPrefix + Guid.NewGuid().ToString()[0..8].ToUpper()},
                new Booking{ RoomId = h1r2Guid, Guests = 1, StartDate = new DateTime(2025,12,8), EndDate = new DateTime(2025,12,10), BookingReference = hotel1.BookingRefPrefix + Guid.NewGuid().ToString()[0..8].ToUpper()},

                new Booking{ RoomId = h1r3Guid, Guests = 1, StartDate = new DateTime(2025,12,1), EndDate = new DateTime(2025,12,3), BookingReference = hotel1.BookingRefPrefix + Guid.NewGuid().ToString()[0..8].ToUpper()},
                new Booking{ RoomId = h1r3Guid, Guests = 2, StartDate = new DateTime(2025,12,9), EndDate = new DateTime(2025,12,12), BookingReference = hotel1.BookingRefPrefix + Guid.NewGuid().ToString()[0..8].ToUpper()},

                new Booking{ RoomId = h1r5Guid, Guests = 4, StartDate = new DateTime(2025,12,1), EndDate = new DateTime(2025,12,5), BookingReference = hotel1.BookingRefPrefix + Guid.NewGuid().ToString()[0..8].ToUpper()},
                new Booking{ RoomId = h1r5Guid, Guests = 3, StartDate = new DateTime(2025,12,5), EndDate = new DateTime(2025,12,9), BookingReference = hotel1.BookingRefPrefix + Guid.NewGuid().ToString()[0..8].ToUpper()},
                new Booking{ RoomId = h1r5Guid, Guests = 4, StartDate = new DateTime(2025,12,9), EndDate = new DateTime(2025,12,12), BookingReference = hotel1.BookingRefPrefix + Guid.NewGuid().ToString()[0..8].ToUpper()},

                new Booking{ RoomId = h1r6Guid, Guests = 4, StartDate = new DateTime(2025,12,5), EndDate = new DateTime(2025,12,8), BookingReference = hotel1.BookingRefPrefix + Guid.NewGuid().ToString()[0..8].ToUpper()}
            };

            db.Bookings.AddRange(bookings1);
            db.SaveChanges();


            //Hotel 2 - Citrus Cove // 1 Single, 4 Doubles, 1 Deluxe

            var hotel2 = new Hotel { Name = "Regal Crown Hotel", BookingRefPrefix = "RCH" };
            db.Hotels.Add(hotel2);
            db.SaveChanges();


            var rooms2 = new List<Room>
            {
                new Room { HotelId = hotel2.Id, Name = "S1", RoomType = RoomType.Single, Capacity = 1 },
                new Room { HotelId = hotel2.Id, Name = "D1", RoomType = RoomType.Double, Capacity = 2 },
                new Room { HotelId = hotel2.Id, Name = "D2", RoomType = RoomType.Double, Capacity = 2 },
                new Room { HotelId = hotel2.Id, Name = "D3", RoomType = RoomType.Double, Capacity = 2 },
                new Room { HotelId = hotel2.Id, Name = "D4", RoomType = RoomType.Double, Capacity = 2 },
                new Room { HotelId = hotel2.Id, Name = "X1", RoomType = RoomType.Deluxe, Capacity = 4 },
            };


            db.Rooms.AddRange(rooms2);
            db.SaveChanges();

            //Hotel 3 - Citrus Cove - 0 Singles, 3 Doubles, 3 Deluxe

            var hotel3 = new Hotel { Name = "Imperial Heights Resort", BookingRefPrefix = "IHR" };
            db.Hotels.Add(hotel3);
            db.SaveChanges();


            var rooms3 = new List<Room>
            {
                new Room { HotelId = hotel3.Id, Name = "Superior Double", RoomType = RoomType.Double, Capacity = 2 },
                new Room { HotelId = hotel3.Id, Name = "Luxury Double", RoomType = RoomType.Double, Capacity = 2 },
                new Room { HotelId = hotel3.Id, Name = "Imperial Double", RoomType = RoomType.Double, Capacity = 2 },
                new Room { HotelId = hotel3.Id, Name = "Augustus Suite", RoomType = RoomType.Deluxe, Capacity = 4 },
                new Room { HotelId = hotel3.Id, Name = "Octavius Suite", RoomType = RoomType.Deluxe, Capacity = 4 },
                new Room { HotelId = hotel3.Id, Name = "Julius Suite", RoomType = RoomType.Deluxe, Capacity = 4 }
            };


            db.Rooms.AddRange(rooms3);
            db.SaveChanges();

            
        }

        public static void Reset(AppDbContext db)
        {
            db.Hotels.RemoveRange(db.Hotels);
            db.Rooms.RemoveRange(db.Rooms);
            db.Bookings.RemoveRange(db.Bookings);
            db.SaveChanges();
        }
    }
}