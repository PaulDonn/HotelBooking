using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using WaracleHotelBooking.Controllers;
using WaracleHotelBooking.DTOs;
using WaracleHotelBooking.DataModel.Models;
using WaracleHotelBooking.Services;
using WaracleHotelBooking.Tests.Utils;

namespace WaracleHotelBooking.Tests.Controllers
{
    [ExcludeFromCodeCoverage]
    public class HotelControllerTests
    {
        [Fact]
        public async Task CreateBooking_ReturnsOk_WhenNoHotelFound()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new HotelController(db);

            var result = await controller.GetHotels("ZYX");

            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            var model = okObjectResult.Value as List<Hotel>;
            model.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateBooking_ReturnsOk_WhenOneHotelFound()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new HotelController(db);

            var result = await controller.GetHotels("Pinnacle");

            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            var model = okObjectResult.Value as List<Hotel>;
            model.Should().HaveCount(1);
        }

        [Fact]
        public async Task CreateBooking_ReturnsOk_WhenManyHotelFound()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new HotelController(db);

            var result = await controller.GetHotels("S");

            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            var model = okObjectResult.Value as List<Hotel>;
            model.Should().HaveCount(2);
        }

        [Fact]
        public async Task CreateBooking_ReturnsOk_WhenCasingDoesNotMatchFound()
        {
            var db = TestDbContextFactory.CreateContext();
            var controller = new HotelController(db);

            var result = await controller.GetHotels("cRoWn");

            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            var model = okObjectResult.Value as List<Hotel>;
            model.Should().HaveCount(1);
        }
    }
}