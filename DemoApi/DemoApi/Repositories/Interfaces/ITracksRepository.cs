using DemoApi.DTOs;
using DemoApi.Models;

namespace DemoApi.Repositories.Interfaces
{
    public interface ITracksRepository
    {
        Task<Track?> GetByIdAsync(int trackId);

        Task<IEnumerable<Track>> SearchTracksAsync(string? artist = null, int? genreId = null, int? albumId = null, string? trackName = null);

        Task<IEnumerable<GenreSummary>> GetTracksGroupedByGenreAsync();
    }
}