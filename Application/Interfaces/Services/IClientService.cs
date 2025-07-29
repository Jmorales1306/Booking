using Application.DTOs.Client;

namespace Application.Interfaces.Services
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDTo>> GetAll();
        Task<ClientDTo> GetById(int id);
        Task<ClientDTo> Add(ClientInsertDto clientInsertDto);
        Task<bool> Update(ClientUpdateDto clientUpdateDto);
        Task<bool> Delete(int id);
    }
}