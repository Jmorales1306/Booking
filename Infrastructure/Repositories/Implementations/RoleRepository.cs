using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly StoreContext _context;
        public RoleRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetById(int id)
        {
            return await _context.Roles.FindAsync(id);
        }
        public async Task Add(Role role)
        {
            await _context.Roles.AddAsync(role);
        }


        public void Update(Role role)
        {
            _context.Roles.Attach(role);
            _context.Entry(role).State = EntityState.Modified;
        }

        public void Delete(Role role)
        => _context.Roles.Remove(role);

    }

}