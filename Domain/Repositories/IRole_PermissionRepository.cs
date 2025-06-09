using Domain.Models;

namespace Domain.Repositories
{
    public interface IRole_PermissionRepository
    {

        // 1. Añadir una nueva relación Rol-Permiso
        // Recibe la entidad RolePermission y la añade a la base de datos.
        Task Add(Role_Permission rolePermission);

        // 2. Eliminar una relación Rol-Permiso específica
        // Para una tabla de unión, la clave primaria es compuesta,
        // por lo que se necesitan ambos IDs para identificar la relación a eliminar.
        Task<bool> Delete(int roleId, int permissionId);

        // 3. Obtener todas las relaciones Rol-Permiso
        // Podría ser útil para propósitos de administración/depuración,
        // pero quizás no tan común en lógica de negocio directa.
        Task<IEnumerable<Role_Permission>> GetAll();

        // 4. Obtener relaciones de permisos para un rol específico
        // Retorna todos los objetos RolePermission asociados a un determinado roleId.
        Task<IEnumerable<Role_Permission>> GetPermissionsByRoleId(int roleId);

        // 5. Obtener relaciones de roles para un permiso específico
        // Retorna todos los objetos RolePermission asociados a un determinado permissionId.
        Task<IEnumerable<Role_Permission>> GetRolesByPermissionId(int permissionId);

        // 6. Verificar si existe una relación Rol-Permiso específica
        // Útil para evitar duplicados antes de añadir o para verificar existencia.
        Task<bool> Exists(int roleId, int permissionId);

    }
}