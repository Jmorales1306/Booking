using Booking.UseCases.DTOs.User;

namespace Booking.UseCases.Services
{
    public class UserService(IUnitOfWork unitOfWork) : IUserService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var user = await _unitOfWork.Users.GetAll();
            return [.. user.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                RoleId = u.RoleId
            })];
        }
        public async Task<UserDto> GetById(int id)
        {
            var user = await _unitOfWork.Users.GetById(id) ?? throw new KeyNotFoundException($"Usuario con el Id {id} no encontrado");
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleId = user.RoleId
            };
        }
        public async Task<UserDto> Add(UserInsertDto userInsertDto)
        {
            var existEmail = await _unitOfWork.Users.GetByEmail(userInsertDto.Email);
            if (existEmail != null)
            {
                throw new InvalidOperationException($"Ya existe un Usuario con el correo electrónico '{userInsertDto.Email}'.");
            }

            var user = new Core.Entities.User
            {
                FirstName = userInsertDto.FirstName,
                LastName = userInsertDto.LastName,
                Email = userInsertDto.Email,
                RoleId = userInsertDto.RoleId
            };

            await _unitOfWork.Users.Add(user);
            await _unitOfWork.Complete();

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleId = user.RoleId
            };
        }
        public async Task<bool> Update(UserUpdateDto userUpdateDto)
        {
            var existUser = await _unitOfWork.Users.GetById(userUpdateDto.Id);
            if (existUser == null)
            {
                return false;
            }
            var usertWithSameEmail = await _unitOfWork.Users.GetByEmail(userUpdateDto.Email);
            if (usertWithSameEmail != null && usertWithSameEmail.Id != userUpdateDto.Id)
            {
                throw new InvalidOperationException($"Ya existe otro Usuario con el correo electrónico '{userUpdateDto.Email}'.");
            }

            existUser.FirstName = userUpdateDto.FirstName;
            existUser.LastName = userUpdateDto.LastName;
            existUser.Email = userUpdateDto.Email;
            existUser.RoleId = userUpdateDto.RoleId;

            _unitOfWork.Users.Update(existUser);
            var rowsAffected = await _unitOfWork.Complete();
            return rowsAffected > 0;
        }
        public async Task<bool> Delete(int id)
        {
            var userToDelete = await _unitOfWork.Users.GetById(id);
            if (userToDelete == null)
            {
                return false;
            }

            _unitOfWork.Users.Delete(userToDelete);
            var rowsAffected = await _unitOfWork.Complete();
            return rowsAffected > 0;
        }
    }
}