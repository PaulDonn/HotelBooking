using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaracleHotelBooking.DataModel.Data;

namespace WaracleHotelBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly AppDbContext _db;
        public HotelController(AppDbContext db) => _db = db;


        /// <summary>
        /// Gets a list of hotel records by name
        /// </summary>
        /// <returns>A list of hotel records.</returns>
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
