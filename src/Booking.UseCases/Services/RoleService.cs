using Booking.UseCases.DTOs.Role;

namespace Booking.UseCases.Services
{
    public class RoleService(IUnitOfWork unitOfWork) : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<IEnumerable<RoleDto>> GetAll()
        {
            var role = await _unitOfWork.Roles.GetAll();
            return [.. role.Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name
            })];
        }

        public async Task<RoleDto> GetById(int id)
        {
            var role = await _unitOfWork.Roles.GetById(id) ?? throw new DomainException($"Rol con el Id {id} no encontrado", code: "ROLE_NOT_FOUND");
            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public async Task<RoleDto> Add(RoleInsertDto roleInsertDto)
        {
            var existRole = await _unitOfWork.Roles.GetByName(roleInsertDto.Name);
            if (existRole != null)
            {
                throw new DomainException($"Ya existe un Rol con el nombre {roleInsertDto.Name}.", code: "ROLE_NAME_ALREADY_EXISTS");
            }
            var role = new Core.Entities.Role
            {
                Name = roleInsertDto.Name
            };

            await _unitOfWork.Roles.Add(role);
            await _unitOfWork.Complete();

            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public async Task<bool> Update(RoleUpdateDto roleUpdateDto)
        {
            var existRole = await _unitOfWork.Roles.GetById(roleUpdateDto.Id) ?? throw new DomainException($"No se encontro un Rol con el Id:{roleUpdateDto.Id}.", code: "ROLE_NOT_FOUND");

            existRole.Name = roleUpdateDto.Name;
            _unitOfWork.Roles.Update(existRole);
            var rowsAffected = await _unitOfWork.Complete();
            return rowsAffected > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var roleToDelete = await _unitOfWork.Roles.GetById(id) ?? throw new DomainException($"No se encontro un Rol con el Id:{id}.", code: "ROLE_NOT_FOUND");

            _unitOfWork.Roles.Delete(roleToDelete);
            var rowsAffected = await _unitOfWork.Complete();
            return rowsAffected > 0;
        }
    }
}