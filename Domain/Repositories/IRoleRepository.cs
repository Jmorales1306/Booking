using Domain.Models;

namespace Domain.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetById(int id);
        Task<IEnumerable<Role>> GetAll();
        Task Add(Role role);
        Task Update(Role role);
        Task<bool> Delete(int id);
        Task<bool> Exists(int id);
        Task<bool> ExistsByName(string name);
        Task<Role?> GetByName(string name);
    }
}