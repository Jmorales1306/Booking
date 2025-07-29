using Domain.Repositories;

namespace Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository Clients { get; }
        IRoleRepository Roles { get; }
        IPermissionRepository Permissions { get; }
        IRoomRepository Rooms { get; }
        ILocationRepository Locations { get; }
        IUserRepository Users { get; }
        IBookingRepository Bookings { get; }
        Task<int> Complete();
    }
}