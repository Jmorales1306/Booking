using Domain.Interface;
using Domain.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly StoreContext _context;

        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        //Instancias de los repositorios
        private IRoleRepository? _roles;
        private IPermissionRepository? _permissions;
        private IUserRepository? _users;
        private ILocationRepository? _locations;
        private IRoomRepository? _rooms;
        private IClientRepository? _clients;
        private IBookingRepository? _bookings;
        private IRole_PermissionRepository? _rolePermissions;

        //Acceso a repositorios

        /// <summary>
        /// https://learn.microsoft.com/es-es/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
        // public IRoleRepository Roles
        // {
        //     get
        //     {
        //         if (_roles == null)
        //         {
        //             _roles = new RoleRepository(_context);
        //         }
        //     }
        // }
        /// </summary>
        /// 
        public IRoleRepository Roles => _roles ??= new RoleRepository(_context);
        public IPermissionRepository Permissions => _permissions ??= new PermissionRepository(_context);
        public IUserRepository Users => _users ??= new UserRepository(_context);
        public ILocationRepository Locations => _locations ??= new LocationRepository(_context);
        public IRoomRepository Rooms => _rooms ??= new RoomRepository(_context);
        public IClientRepository Clients => _clients ??= new ClientRepository(_context);
        public IBookingRepository Bookings => _bookings ??= new BookingRepository(_context);
        public IRole_PermissionRepository RolePermissions => _rolePermissions ??= new Role_PermissionRepository(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}