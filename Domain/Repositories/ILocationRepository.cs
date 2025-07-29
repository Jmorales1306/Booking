using Domain.Models;

namespace Domain.Repositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<Location?> GetByName(string name);
    }
}