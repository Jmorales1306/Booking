namespace Booking.Core.Interface
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role?> GetByName(string name);
    }
}