using DemoApi.DTOs;
using DemoApi.Models;
using DemoApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class TracksController(IMemoryCache cache, ITracksRepository repository) : ControllerBase
    {
        [HttpGet(Name = "{id}")]
        public async Task<ActionResult<Track?>> GetTrackById(int id)
        {
            var track = await repository.GetByIdAsync(id);
            return Ok(track);
        }

        [HttpGet("summary")]
        public async Task<ActionResult<IEnumerable<GenreSummary>>> GetTracksSummary()
        {
            var summary = await repository.GetTracksGroupedByGenreAsync();
            return Ok(summary);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Track>?>> SearchTracks(
            [FromQuery] string? artist = null,
            [FromQuery] int? genreId = null,
            [FromQuery] int? albumId = null,
            [FromQuery] string? trackName = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            string cacheKey = $"SearchTracks_{artist}_{genreId}_{albumId}_{trackName}_{pageNumber}_{pageSize}";

            if (!TryGetCachedTracks(cacheKey, out List<Track>? cachedTracks))
            {
                var tracks = await repository.SearchTracksAsync(artist, genreId, albumId, trackName);

                cachedTracks = [.. tracks
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)];

                SetCache(cacheKey, cachedTracks);
            }

            return Ok(cachedTracks);
        }


        private bool TryGetCachedTracks(string cacheKey, out List<Track>? cachedTracks)
        {
            return cache.TryGetValue(cacheKey, out cachedTracks);
        }

        private void SetCache(string cacheKey, List<Track> tracks)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));

            cache.Set(cacheKey, tracks, cacheEntryOptions);
        }
    }
}