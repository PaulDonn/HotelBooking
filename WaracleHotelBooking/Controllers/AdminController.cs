using Microsoft.AspNetCore.Mvc;
using WaracleHotelBooking.Data;

namespace WaracleHotelBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _db;
        public AdminController(AppDbContext db) => _db = db;


        [HttpPost("reset")]
        public IActionResult Reset()
        {
            DbSeeder.Reset(_db);
            return Ok();
        }


        [HttpPost("seed")]
        public IActionResult Seed()
        {
            DbSeeder.Seed(_db);
            return Ok();
        }
    }
}
