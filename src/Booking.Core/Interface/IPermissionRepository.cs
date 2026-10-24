namespace Booking.Core.Interface
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        Task<Permission?> GetByName(string name);
    }
}