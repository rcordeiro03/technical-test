using DemoApi.Models;

namespace DemoApi.Repositories.Interfaces
{
    public interface IGenresRepository
    {
        Task<IEnumerable<Genre>> GetAllAsync();
    }
}