using Domain.Models;

namespace Domain.Repositories
{
    public interface IRolePermission
    {
        Task<IEnumerable<RolePermission>> GetAll();
        Task<IEnumerable<RolePermission>> GetPermissionsByRoleId(int roleId);
        Task Add(RolePermission rolePermission);
        Task<bool> Delete(int roleId, int permissionId);
        Task<IEnumerable<RolePermission>> GetRolesByPermissionId(int permissionId);
        Task<bool> Exists(int roleId, int permissionId);
        Task<bool> ExistsAnyRoleWithPermission(int permissionId);
    }
}