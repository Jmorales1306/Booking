using Booking.UseCases.DTOs.Location;

namespace Booking.UseCases.Services
{
    public class LocationService(IUnitOfWork unitOfWork) : ILocationService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<LocationDto>> GetAll()
        {
            var location = await _unitOfWork.Locations.GetAll();
            return [.. location.Select(l => new LocationDto
            {
                Id = l.Id,
                Name = l.Name,
                Address = l.Address,
                Description = l.Description
            })];
        }

        public async Task<LocationDto> GetById(int id)
        {
            var location = await _unitOfWork.Locations.GetById(id) ?? throw new KeyNotFoundException($"Ubication con el Id {id} no encontrado");
            return new LocationDto
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
                Description = location.Address
            };
        }

        public async Task<LocationDto> Add(LocationInsertDto locationInsertDto)
        {
            var existLocation = await _unitOfWork.Locations.GetByName(locationInsertDto.Name);
            if (existLocation != null)
            {
                throw new InvalidOperationException($"Ya existe una ubicacion con el nombre {locationInsertDto.Name}.");
            }
            var location = new Core.Entities.Location
            {
                Name = locationInsertDto.Name,
                Address = locationInsertDto.Address,
                Description = locationInsertDto.Description
            };

            await _unitOfWork.Locations.Add(location);
            await _unitOfWork.Complete();

            return new LocationDto
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
                Description = location.Address
            };
        }

        public async Task<bool> Update(LocationUpdateDto locationUpdateDto)
        {
            var existLocation = await _unitOfWork.Locations.GetById(locationUpdateDto.Id);

            if (existLocation == null)
            {
                throw new KeyNotFoundException($"No se encontró una ubicación con el ID '{locationUpdateDto.Id}'.");
            }

            existLocation.Name = locationUpdateDto.Name;
            existLocation.Address = locationUpdateDto.Address;
            existLocation.Description = locationUpdateDto.Description;

            _unitOfWork.Locations.Update(existLocation);
            var rowsAffected = await _unitOfWork.Complete();

            return rowsAffected > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var locationToDelete = await _unitOfWork.Locations.GetById(id) ?? throw new InvalidOperationException($"No se encontro una ubicacion con el Id:{id}.");

            _unitOfWork.Locations.Delete(locationToDelete);
            var rowsAffected = await _unitOfWork.Complete();
            return rowsAffected > 0;
        }
    }
}