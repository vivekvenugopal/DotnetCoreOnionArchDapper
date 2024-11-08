using Demo.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API fetch for all");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok($"API for get by id: {id}");
        }

        [HttpPost]
        public IActionResult Create()
        {
            return Ok("Testing post request ok.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id)
        {
            return Ok($"Testing PUT request ok with ID: {id}");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            return Ok($"Deleting PUT request ok  ID: {id}");
        }
    }
}
