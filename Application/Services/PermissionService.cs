using Application.DTOs.Permission;
using Application.Interfaces.Services;
using Domain.Interface;
using Domain.Models;

namespace Application.Services
{
    public class PermissionService(IUnitOfWork unitOfWork) : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;


        public async Task<IEnumerable<PermissionDTo>> GetAll()
        {
            var permission = await _unitOfWork.Permissions.GetAll();

            return [.. permission.Select(p => new PermissionDTo
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            })];

        }

        public async Task<PermissionDTo> GetById(int id)
        {
            var permission = await _unitOfWork.Permissions.GetById(id) ?? throw new KeyNotFoundException($"Permiso con el id {id} no encontrado.");
            return new PermissionDTo
            {
                Id = permission.Id,
                Name = permission.Name,
                Description = permission.Description
            };
        }

        public async Task<PermissionDTo> Add(PermissionInsertDTo permissionInsertDTo)
        {
            var existPermision = await _unitOfWork.Permissions.GetByName(permissionInsertDTo.Name);
            if (existPermision != null)
            {
                throw new InvalidOperationException($"Ya existe un Permiso con el nombre {permissionInsertDTo.Name}.");
            }

            var permission = new Permission
            {
                Name = permissionInsertDTo.Name,
                Description = permissionInsertDTo.Description
            };

            await _unitOfWork.Permissions.Add(permission);
            await _unitOfWork.Complete();

            return new PermissionDTo
            {
                Id = permission.Id, 
                Name = permission.Name,
                Description = permission.Description
            };

        }

        public async Task<bool> Update(PermissionUpdateDTo permissionUpdateDTo)
        {
            var existPermision = await _unitOfWork.Permissions.GetById(permissionUpdateDTo.Id) ?? throw new InvalidOperationException($"No se encontro el Permiso con el Id:{permissionUpdateDTo.Id}.");

            existPermision.Name = permissionUpdateDTo.Name;
            existPermision.Description = permissionUpdateDTo.Description;

            _unitOfWork.Permissions.Update(existPermision);
            var rowsAffected = await _unitOfWork.Complete();
            return rowsAffected > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var permissionToDelete = await _unitOfWork.Permissions.GetById(id) ?? throw new InvalidOperationException($"No se encontro el Permiso con el Id:{id}.");

            _unitOfWork.Permissions.Delete(permissionToDelete);
            var rowsAffected = await _unitOfWork.Complete();
            return rowsAffected > 0;
        }
    }
}