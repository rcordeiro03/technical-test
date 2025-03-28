using DemoApi.Models;
using DemoApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TracksController
    {
        [HttpGet(Name = "{id}")]
        public async Task<Track> GetTrackById(int id)
        {
            var repository = new TracksRepository();
            var tracks = await repository.GetById(id);
            return tracks;
        }
    }
}
