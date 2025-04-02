using Microsoft.Data.Sqlite;

namespace DemoApi.Repositories.Base
{
    public abstract class BaseRepository
    {
        private readonly string _connectionString = @"Data Source=Assets\chinook.db";

        protected async Task<SqliteConnection> GetOpenConnectionAsync()
        {
            var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}