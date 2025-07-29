using Domain.Models;

namespace Domain.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role?> GetByName(string name);
    }
}