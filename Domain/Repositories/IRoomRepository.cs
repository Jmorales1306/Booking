using Domain.Models;

namespace Domain.Repositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<Room?> GetByName(string name);
    }
}