using DemoApi.Models;

namespace DemoApi.Repositories.Interfaces
{
    public interface IArtistsRepository
    {
        Task<IEnumerable<Artist>> GetAllAsync();
    }
}