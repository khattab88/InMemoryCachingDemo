using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public DriversController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Driver driver) 
        {
            if(!ModelState.IsValid) { return BadRequest(); }

            _db.Drivers.Add(driver);
            await _db.SaveChangesAsync();

            return Created("", driver);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var drivers = await _db.Drivers.ToListAsync();

            return Ok(drivers);
        }
    }
}
