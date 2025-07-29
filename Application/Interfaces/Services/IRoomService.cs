using Application.DTOs.Room;

namespace Application.Interfaces.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDTo>> GetAll();
        Task<RoomDTo> GetById(int id);
        Task<RoomDTo> Add(RoomInsertDto roomInsertDto);
        Task<bool> Update(RoomUpdateDto roomUpdateDto);
        Task<bool> Delete(int id);
    }
}