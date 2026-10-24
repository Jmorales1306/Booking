using Booking.UseCases.DTOs.Permission;

namespace Booking.UseCases.Interfaces
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionDto>> GetAll();
        Task<PermissionDto> GetById(int id);
        Task<PermissionDto> Add(PermissionInsertDto permissionInsertDTo);
        Task<bool> Update(PermissionUpdateDto permissionUpdateDTo);
        Task<bool> Delete(int id);
    }
}