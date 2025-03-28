using Dapper;
using DemoApi.Models;
using Microsoft.Data.Sqlite;

namespace DemoApi.Repositories
{
    public class TracksRepository
    {
        public async Task<Track?> GetById(int trackId)
        {
            var query = "SELECT * FROM tracks WHERE TrackId = @Trackid";
            using var connection = new SqliteConnection(@"Data Source=Assets\chinook.db");
            await connection.OpenAsync();
            var track = await connection.QueryAsync<Track>(query, new { Trackid = trackId });
            return track.SingleOrDefault();
        }
    }
}
