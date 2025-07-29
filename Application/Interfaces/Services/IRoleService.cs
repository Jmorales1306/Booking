using Application.DTOs.Role;

namespace Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAll();
        Task<RoleDto> GetById(int id);
        Task<RoleDto> Add(RoleInsertDto roleInsertDto);
        Task<bool> Update(RoleUpdateDto roleUpdateDto);
        Task<bool> Delete(int id);
    }
}