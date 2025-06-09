using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly StoreContext _context;

        public PermissionRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Permission>> GetAll()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<Permission> GetById(int id)
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
        => _context.Permissions.Remove(permission);
    }
}