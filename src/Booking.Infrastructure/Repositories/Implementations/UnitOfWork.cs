namespace Booking.Infrastructure.Repositories.Implementations
{
    public class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
    {
        private readonly AppDbContext _context = appDbContext;

        private IClientRepository? _clients;
        private IRoleRepository? _roles;
        private IPermissionRepository? _permissions;
        private IRoomRepository? _rooms;
        private ILocationRepository? _locations;
        private IUserRepository? _users;
        private IBookingRepository? _bookings;

        public IClientRepository Clients => _clients ??= new ClientRepository(_context);
        public IRoleRepository Roles => _roles ??= new RoleRepository(_context);
        public IPermissionRepository Permissions => _permissions ??= new PermissionRepository(_context);
        public IRoomRepository Rooms => _rooms ??= new RoomRepostitory(_context);

        public ILocationRepository Locations => _locations ??= new LocationRepository(_context);

        public IUserRepository Users => _users ??= new UserRepository(_context);

        public IBookingRepository Bookings => _bookings ??= new BookingRepository(_context);

        public async Task<int> Complete()
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