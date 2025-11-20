using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaracleHotelBooking.Data;

namespace WaracleHotelBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly AppDbContext _db;
        public HotelController(AppDbContext db) => _db = db;


        [HttpGet]
        public async Task<IActionResult> GetHotels([FromQuery] string name)
        {
            var hotels = await _db.Hotels.Include(x => x.Rooms)
            .Where(h => h.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

            return Ok(hotels);
        }
    }
}
