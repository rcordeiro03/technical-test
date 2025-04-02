using DemoApi.Models;
using DemoApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("api/v1/artists")]
    public class ArtistsController(IArtistsRepository artistsRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetAll()
        {
            var artists = await artistsRepository.GetAllAsync();
            return Ok(artists);
        }
    }
}