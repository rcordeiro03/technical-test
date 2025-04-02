using Dapper;
using DemoApi.DTOs;
using DemoApi.Models;
using DemoApi.Repositories.Base;
using DemoApi.Repositories.Interfaces;

namespace DemoApi.Repositories
{
    public class TracksRepository : BaseRepository, ITracksRepository
    {
        public async Task<Track?> GetByIdAsync(int trackId)
        {
            var query = @"
                SELECT 
                    TrackId, 
                    Name, 
                    AlbumId, 
                    MediaTypeId, 
                    GenreId, 
                    Composer, 
                    Milliseconds, 
                    Bytes, 
                    UnitPrice 
                FROM tracks 
                WHERE TrackId = @Trackid";

            using var connection = await GetOpenConnectionAsync();
            return await connection.QuerySingleAsync<Track>(query, new { Trackid = trackId });
        }

        public async Task<IEnumerable<Track>> SearchTracksAsync(string? artist = null, int? genreId = null, int? albumId = null, string? trackName = null)
        {
            var query = @"
                SELECT 
                    TrackId, 
                    Name, 
                    AlbumId, 
                    MediaTypeId, 
                    GenreId, 
                    Composer, 
                    Milliseconds, 
                    Bytes, 
                    UnitPrice 
                FROM tracks 
                WHERE 
                    (@Artist IS NULL OR Composer LIKE @Artist) AND
                    (@GenreId IS NULL OR GenreId = @GenreId) AND
                    (@AlbumId IS NULL OR AlbumId = @AlbumId) AND
                    (@TrackName IS NULL OR Name LIKE @TrackName)";

            using var connection = await GetOpenConnectionAsync();
            return await connection.QueryAsync<Track>(query, new { Artist = artist, GenreId = genreId, AlbumId = albumId, TrackName = trackName });
        }

        public async Task<IEnumerable<GenreSummary>> GetTracksGroupedByGenreAsync()
        {
            var query = @"
                SELECT 
                    GenreId, 
                    COUNT(*) AS Count 
                FROM tracks 
                GROUP BY GenreId";

            using var connection = await GetOpenConnectionAsync();
            return await connection.QueryAsync<GenreSummary>(query);
        }
    }
}