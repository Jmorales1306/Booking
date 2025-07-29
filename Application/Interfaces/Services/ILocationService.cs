using Application.DTOs.Location;

namespace Application.Interfaces.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDto>> GetAll();
        Task<LocationDto> GetById(int id);
        Task<LocationDto> Add(LocationInsertDto locationInsertDto);
        Task<bool> Update(LocationUpdateDto locationUpdateDto);
        Task<bool> Delete(int id);
    }
}