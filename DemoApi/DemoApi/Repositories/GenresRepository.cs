using Dapper;
using DemoApi.Models;
using DemoApi.Repositories.Base;
using DemoApi.Repositories.Interfaces;

namespace DemoApi.Repositories
{
    public class GenresRepository : BaseRepository, IGenresRepository
    {
        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            var query = @"
                SELECT 
                    GenreId, 
                    Name
                FROM Genres";

            using var connection = await GetOpenConnectionAsync();
            return await connection.QueryAsync<Genre>(query);  
        }
    }
}