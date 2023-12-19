using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ApplicationDbContext ad;

        public CityController(ApplicationDbContext ad)
        {
            this.ad = ad;
        }

        // GET: api/City
        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var cities = await ad.Cities.ToListAsync();
            return Ok(cities);
        }

        //POST api/City/add?cityName=Miami
        //POST: api/City/add/Texas
        [HttpPost("add")]
        [HttpPost("add/{cityName}")]
        public async Task<IActionResult> AddCity(string cityName)
        {
            City city = new City();
            city.Name = cityName;
            await ad.Cities.AddAsync(city);

            await ad.SaveChangesAsync();

            return Ok(city);
        }

        //PUT api/City/update/1
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] City updatedCity)
        {
            var existingCity = await ad.Cities.FindAsync(id);
            existingCity.Name = updatedCity.Name;

            await ad.SaveChangesAsync();

            return Ok(existingCity);
        }

        //DELETE api/City/delete/Miami
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await ad.Cities.FindAsync(id);
            ad.Cities.Remove(city);
            await ad.SaveChangesAsync();

            return Ok(id);

        }
    }
}