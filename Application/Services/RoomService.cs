using Application.DTOs.Room;
using Application.Interfaces.Services;
using Domain.Interface;
using Domain.Models;

namespace Application.Services
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

        public async Task<RoomDTo> GetById(int id)
        {
            var room = await _unitOfWork.Rooms.GetById(id) ?? throw new KeyNotFoundException($"Room con el Id:{id} no encontrado");
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
                throw new InvalidOperationException($"Ya existe un Rol con el nombre {roomInsertDto.Name}.");
            }

            var existingLocation = await _unitOfWork.Locations.GetById(roomInsertDto.LocatonId);
            if (existingLocation == null)
            {
                throw new KeyNotFoundException($"La Ubicación con el ID '{roomInsertDto.LocatonId}' no existe.");
            }

            var room = new Room
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
            var existRoom = await _unitOfWork.Rooms.GetById(roomUpdateDto.Id) ?? throw new InvalidOperationException($"No se encontro una Sala con el Id:{roomUpdateDto.Id}.");

            existRoom.Name = roomUpdateDto.Name;
            existRoom.Capacity = roomUpdateDto.Capacity;
            existRoom.LocationId = roomUpdateDto.LocatonId;

            _unitOfWork.Rooms.Update(existRoom);
            var rowsAffected = await _unitOfWork.Complete();
            return rowsAffected > 0;

        }
        public async Task<bool> Delete(int id)
        {
            var roomToDelete = await _unitOfWork.Rooms.GetById(id) ?? throw new InvalidOperationException($"No se encontro una Sala con el Id:{id}.");

            _unitOfWork.Rooms.Delete(roomToDelete);
            var rowsAffected = await _unitOfWork.Complete();
            return rowsAffected > 0;
        }
    }
}