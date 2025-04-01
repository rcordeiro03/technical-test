using DemoApi.Models;
using DemoApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("api/v1/artists")]
    public class ArtistsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetAll()
        {
            var repository = new ArtistsRepository();
            var artists = await repository.GetAsync();
            return Ok(artists);
        }
    }
}
