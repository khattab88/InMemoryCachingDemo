using API.Data;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        private readonly ApplicationDbContext _db;

        public DriversController(ICacheService cacheService, ApplicationDbContext db)
        {
            _cacheService = cacheService;
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
            var cachedDrivers = _cacheService.GetData<IEnumerable<Driver>>(CacheKeys.Drivers);

            if(cachedDrivers != null && cachedDrivers.Count() > 0) 
            {
                return Ok(cachedDrivers);
            }

            var drivers = await _db.Drivers.ToListAsync();

            var expirationTime = DateTimeOffset.Now.AddMinutes(2);
            _cacheService.SetData<IEnumerable<Driver>>(CacheKeys.Drivers, drivers, expirationTime);

            return Ok(drivers);
        }
    }
}
