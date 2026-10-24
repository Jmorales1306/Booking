using Booking.UseCases.DTOs.Client;

namespace Booking.UseCases.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAll();
        Task<ClientDto> GetById(int id);
        Task<ClientDto> Add(ClientInsertDto clientInsertDto);
        Task<bool> Update(ClientUpdateDto clientUpdateDto);
        Task<bool> Delete(int id);
    }
}