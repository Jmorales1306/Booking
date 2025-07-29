using Domain.Models;

namespace Domain.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client?> GetByEmail(string email);
        Task<Client?> GetByPhoneNumber(string phoneNumber);
    }
}