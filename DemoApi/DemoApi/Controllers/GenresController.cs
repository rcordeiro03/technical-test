using DemoApi.Models;
using DemoApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GenresController(IGenresRepository genresRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            var genres = await genresRepository.GetAllAsync();
            return Ok(genres);
        }
    }
}