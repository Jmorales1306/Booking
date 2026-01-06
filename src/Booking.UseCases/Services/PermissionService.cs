using Booking.UseCases.DTOs.Permission;

namespace Booking.UseCases.Services
{
    public class PermissionService(IUnitOfWork unitOfWork) : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;


        public async Task<IEnumerable<PermissionDto>> GetAll()
        {
            var permission = await _unitOfWork.Permissions.GetAll();

            return [.. permission.Select(p => new PermissionDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            })];

        }

        public async Task<PermissionDto> GetById(int id)
        {
            var permission = await _unitOfWork.Permissions.GetById(id) ?? throw new DomainException("PERMISSION_NOT_FOUND");
            return new PermissionDto
            {
                Id = permission.Id,
                Name = permission.Name,
                Description = permission.Description
            };
        }

        public async Task<PermissionDto> Add(PermissionInsertDto permissionInsertDTo)
        {
            var existPermision = await _unitOfWork.Permissions.GetByName(permissionInsertDTo.Name);
            if (existPermision != null)
            {
                throw new DomainException(
                    "PERMISSION_NAME_ALREADY_EXISTS",
                    target: "name");
            }

            var permission = new Core.Entities.Permission
            {
                Name = permissionInsertDTo.Name,
                Description = permissionInsertDTo.Description
            };

            await _unitOfWork.Permissions.Add(permission);
            await _unitOfWork.Complete();

            return new PermissionDto
            {
                Id = permission.Id,
                Name = permission.Name,
                Description = permission.Description
            };

        }

        public async Task<bool> Update(PermissionUpdateDto permissionUpdateDTo)
        {
            var existPermision = await _unitOfWork.Permissions.GetById(permissionUpdateDTo.Id) ?? throw new DomainException("PERMISSION_NOT_FOUND");

            existPermision.Name = permissionUpdateDTo.Name;
            existPermision.Description = permissionUpdateDTo.Description;

            _unitOfWork.Permissions.Update(existPermision);
            var rowsAffected = await _unitOfWork.Complete();
            return rowsAffected > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var permissionToDelete = await _unitOfWork.Permissions.GetById(id) ?? throw new DomainException("PERMISSION_NOT_FOUND");

            _unitOfWork.Permissions.Delete(permissionToDelete);
            var rowsAffected = await _unitOfWork.Complete();
            return rowsAffected > 0;
        }
    }
}