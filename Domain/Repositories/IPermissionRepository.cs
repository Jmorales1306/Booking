using Domain.Models;

namespace Domain.Repositories
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        Task<bool> Exists(int id);
        Task<bool> ExistsByName(string name);
        Task<Permission?> GetByName(string name);
    }
}