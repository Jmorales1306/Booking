using Application.DTOs.Permission;

namespace Application.Interfaces.Services
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionDTo>> GetAll();
        Task<PermissionDTo> GetById(int id);
        Task<PermissionDTo> Add(PermissionInsertDTo permissionInsertDTo);
        Task<bool> Update(PermissionUpdateDTo permissionUpdateDTo);
        Task<bool> Delete(int id);
    }
}