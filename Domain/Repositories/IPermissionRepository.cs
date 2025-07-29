using Domain.Models;

namespace Domain.Repositories
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        Task<Permission?> GetByName(string name);
    }
}