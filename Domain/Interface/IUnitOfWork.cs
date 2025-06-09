using Domain.Repositories;

namespace Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRoleRepository Roles { get; }
        IPermissionRepository Permissions { get; }
        IUserRepository Users { get; }
        ILocationRepository Locations { get; }
        IRoomRepository Rooms { get; }
        IClientRepository Clients { get; }
        IBookingRepository Bookings { get; }
        IRole_PermissionRepository RolePermissions { get; }

        /// <summary>
        /// </summary>
        /// <returns></returns>

        Task<int> CompleteAsync();

    }
}