using Application.DTOs.User;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> GetById(int id);
        Task<UserDto> Add(UserInsertDto userInsertDto);
        Task<bool> Update(UserUpdateDto userUpdateDto);
        Task<bool> Delete(int id);
    }
}