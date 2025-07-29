using Application.DTOs.Client;
using Application.DTOs.Role;
using Application.Interfaces.Services;
using Domain.Interface;
using Domain.Models;

namespace Application.Services
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
            var role = await _unitOfWork.Roles.GetById(id) ?? throw new KeyNotFoundException($"Rol con el Id {id} no encontrado");
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
                throw new InvalidOperationException($"Ya existe un Rol con el nombre {roleInsertDto.Name}.");
            }
            var role = new Role
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
            var existRole = await _unitOfWork.Roles.GetById(roleUpdateDto.Id) ?? throw new InvalidOperationException($"No se encontro un Rol con el Id:{roleUpdateDto.Id}.");

            existRole.Name = roleUpdateDto.Name;
            _unitOfWork.Roles.Update(existRole);
            var rowsAffected = await _unitOfWork.Complete();
            return rowsAffected > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var roleToDelete = await _unitOfWork.Roles.GetById(id) ?? throw new InvalidOperationException($"No se encontro un Rol con el Id:{id}.");

            _unitOfWork.Roles.Delete(roleToDelete);
            var rowsAffected = await _unitOfWork.Complete();
            return rowsAffected > 0;
        }
    }
}