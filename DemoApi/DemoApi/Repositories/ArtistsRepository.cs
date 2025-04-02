using Dapper;
using DemoApi.Models;
using DemoApi.Repositories.Base;
using DemoApi.Repositories.Interfaces;

namespace DemoApi.Repositories
{
    public class ArtistsRepository : BaseRepository, IArtistsRepository
    {
        public async Task<IEnumerable<Artist>> GetAllAsync()
        {
            var query = "SELECT ArtistId, Name FROM artists ORDER BY Name ASC;";
            using var connection = await GetOpenConnectionAsync();
            return await connection.QueryAsync<Artist>(query);
        }
    }
}