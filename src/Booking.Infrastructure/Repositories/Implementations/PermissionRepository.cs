using Booking.Core.Entities;

namespace Booking.Infrastructure.Repositories.Implementations
{
    public class PermissionRepository(AppDbContext appDbContext) : IPermissionRepository
    {
        private readonly AppDbContext _context = appDbContext;
        public async Task<IEnumerable<Permission>> GetAll()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<Permission?> GetById(int id)
        {
            return await _context.Permissions.FindAsync(id);
        }

        public async Task Add(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
        }

        public void Update(Permission permission)
        {
            _context.Permissions.Attach(permission);
            _context.Entry(permission).State = EntityState.Modified;
        }

        public void Delete(Permission permission)
        {
            _context.Permissions.Remove(permission);
        }

        public async Task<Permission?> GetByName(string name)
        {
            return await _context.Permissions.FirstOrDefaultAsync(r => r.Name == name);
        }
    }
}