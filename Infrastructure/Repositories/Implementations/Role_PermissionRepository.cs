using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class Role_PermissionRepository : IRole_PermissionRepository
    {
        private readonly StoreContext _context;

        public Role_PermissionRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role_Permission>> GetAll()
        {
            return await _context.Role_Permissions
                        .Include(rp => rp.Role)
                        .Include(rp => rp.Permission)
                        .ToListAsync();
        }

        public async Task<Role_Permission> GetById(int id)
        {
            return await _context.Role_Permissions
                        .Include(rp => rp.Role)
                        .Include(rp => rp.Permission)
                        .FirstOrDefaultAsync(rp => rp.RoleId == id);
        }

        public async Task Add(Role_Permission role_Permission)
        {
            await _context.Role_Permissions.AddAsync(role_Permission);
        }

        public void Update(Role_Permission role_Permission)
        {
            _context.Role_Permissions.Attach(role_Permission);
            _context.Entry(role_Permission).State = EntityState.Modified;
        }

        public void Delete(Role_Permission role_Permission)
        => _context.Role_Permissions.Remove(role_Permission);

        public async Task<IEnumerable<Permission>> GetPermissionsByRoleId(int roleId)
        {
            return await _context.Role_Permissions
                                 .Where(rp => rp.RoleId == roleId)
                                 .Select(rp => rp.Permission)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetRolesByPermissionId(int permissionId)
        {
            return await _context.Role_Permissions
                                 .Where(rp => rp.PermissionId == permissionId)
                                 .Select(rp => rp.Role)
                                 .ToListAsync();
        }

        public async Task AddRolePermission(int roleId, int permissionId)
        {
            var role_Permission = new Role_Permission
            {
                RoleId = roleId,
                PermissionId = permissionId
            };

            await _context.Role_Permissions.AddAsync(role_Permission);
        }

        public void RemoveRolePermission(int roleId, int permissionId)
        {
            var rolePermissionRmv = _context.Role_Permissions
                                            .FirstOrDefault(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

            if (rolePermissionRmv != null)
            {
                _context.Role_Permissions.Remove(rolePermissionRmv);
            }
        }

        public async Task<bool> HasPermission(int roleId, int permissionId)
        {
            return await _context.Role_Permissions
            .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
        }
    }
}