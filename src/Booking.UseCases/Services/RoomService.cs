using Booking.UseCases.DTOs.Room;

namespace Booking.UseCases.Services
{
    public class RoomService(IUnitOfWork unitOfWork) : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<RoomDTo>> GetAll()
        {
            var room = await _unitOfWork.Rooms.GetAll();
            return [.. room.Select(r => new RoomDTo
            {
                Id = r.Id,
                Name = r.Name,
                Capacity = r.Capacity,
                LocatonId = r.LocationId
            })];
        }

        public async Task<RoomDTo?> GetById(int id)
        {
            var room = await _unitOfWork.Rooms.GetById(id);
            if (room is null)
            {
                return null;
            }

            return new RoomDTo
            {
                Id = room.Id,
                Name = room.Name,
                Capacity = room.Capacity,
                LocatonId = room.LocationId
            };
        }

        public async Task<RoomDTo> Add(RoomInsertDto roomInsertDto)
        {
            var existRoom = await _unitOfWork.Rooms.GetByName(roomInsertDto.Name);
            if (existRoom != null)
            {
                throw new DomainException(
                    $"Ya existe una Sala con el nombre '{roomInsertDto.Name}'.",
                    code: "ROOM_NAME_ALREADY_EXISTS");
            }

            var existingLocation = await _unitOfWork.Locations.GetById(roomInsertDto.LocatonId);
            if (existingLocation == null)
            {
                throw new DomainException(
                    $"La Ubicación con el ID '{roomInsertDto.LocatonId}' no existe.",
                    code: "LOCATION_NOT_FOUND");
            }

            var room = new Core.Entities.Room
            {
                Name = roomInsertDto.Name,
                Capacity = roomInsertDto.Capacity,
                LocationId = roomInsertDto.LocatonId
            };

            await _unitOfWork.Rooms.Add(room);
            await _unitOfWork.Complete();

            return new RoomDTo
            {
                Id = room.Id,
                Name = room.Name,
                Capacity = room.Capacity,
                LocatonId = room.LocationId
            };

        }
        public async Task<bool> Update(RoomUpdateDto roomUpdateDto)
        {
            var existRoom = await _unitOfWork.Rooms.GetById(roomUpdateDto.Id);
            if (existRoom is null)
            {
                return false;
            }

            existRoom.Name = roomUpdateDto.Name;
            existRoom.Capacity = roomUpdateDto.Capacity;
            existRoom.LocationId = roomUpdateDto.LocatonId;

            _unitOfWork.Rooms.Update(existRoom);
            var rowsAffected = await _unitOfWork.Complete();
            return rowsAffected > 0;

        }
        public async Task<bool> Delete(int id)
        {
            var roomToDelete = await _unitOfWork.Rooms.GetById(id);
            if (roomToDelete is null)
            {
                return false;
            }

            _unitOfWork.Rooms.Delete(roomToDelete);
            var rowsAffected = await _unitOfWork.Complete();
            return rowsAffected > 0;
        }
    }
}
