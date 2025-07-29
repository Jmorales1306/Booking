using Domain.Models;

namespace Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmail(string email);
    }
}