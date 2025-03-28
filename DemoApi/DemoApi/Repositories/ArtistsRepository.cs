using Dapper;
using DemoApi.Models;
using Microsoft.Data.Sqlite;

namespace DemoApi.Repositories
{
    public class ArtistsRepository
    {
        public async Task<IEnumerable<Artist>> GetAsync()
        {
            var query = "SELECT ArtistId, Name FROM artists ORDER BY Name ASC;";
            using var connection = new SqliteConnection(@"Data Source=Assets\chinook.db");
            await connection.OpenAsync();
            var artists = (await connection.QueryAsync<Artist>(query)).AsList();
            return artists;
        }
    }
}
