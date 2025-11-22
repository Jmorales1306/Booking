using Booking.UseCases.DTOs.Client;

namespace Booking.UseCases.Services
{
    public class ClientService(IUnitOfWork unitOfWork) : IClientService
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<ClientDto>> GetAll()
        {
            var client = await _unitOfWork.Clients.GetAll();
            return [.. client.Select(c => new ClientDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            })];
        }

        public async Task<ClientDto> GetById(int id)
        {
            var client = await _unitOfWork.Clients.GetById(id) ?? throw new DomainException($"Cliente con el Id {id} no encontrado", code: "CLIENT_NOT_FOUND");
            return new ClientDto
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber
            };
        }

        public async Task<ClientDto> Add(ClientInsertDto clientInsertDto)
        {
            var existEmail = await _unitOfWork.Clients.GetByEmail(clientInsertDto.Email);
            if (existEmail != null)
            {
                throw new DomainException($"Ya existe un cliente con el correo electrónico '{clientInsertDto.Email}'.", code: "CLIENT_EMAIL_ALREADY_EXISTS");
            }
            if (!string.IsNullOrWhiteSpace(clientInsertDto.PhoneNumber))
            {
                var existClientByPhone = await _unitOfWork.Clients.GetByPhoneNumber(clientInsertDto.PhoneNumber);
                if (existClientByPhone != null)
                {
                    throw new DomainException($"Ya existe un cliente con el número de teléfono '{clientInsertDto.PhoneNumber}'.", code: "CLIENT_PHONE_ALREADY_EXISTS");
                }
            }

            var client = new Booking.Core.Entities.Client
            {
                FirstName = clientInsertDto.FirstName,
                LastName = clientInsertDto.LastName,
                Email = clientInsertDto.Email,
                PhoneNumber = clientInsertDto.PhoneNumber
            };

            await _unitOfWork.Clients.Add(client);
            await _unitOfWork.Complete();

            return new ClientDto
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber
            };
        }

        public async Task<bool> Update(ClientUpdateDto clientUpdateDto)
        {
            var existClient = await _unitOfWork.Clients.GetById(clientUpdateDto.Id);
            if (existClient == null)
            {
                return false;
            }
            var clientWithSameEmail = await _unitOfWork.Clients.GetByEmail(clientUpdateDto.Email);
            if (clientWithSameEmail != null && clientWithSameEmail.Id != clientUpdateDto.Id)
            {
                throw new DomainException($"Ya existe otro cliente con el correo electrónico '{clientUpdateDto.Email}'.", code: "CLIENT_EMAIL_ALREADY_EXISTS");
            }

            if (!string.IsNullOrWhiteSpace(clientUpdateDto.PhoneNumber))
            {
                var clientWithSamePhone = await _unitOfWork.Clients.GetByPhoneNumber(clientUpdateDto.PhoneNumber);
                if (clientWithSamePhone != null && clientWithSamePhone.Id != clientUpdateDto.Id)
                {
                    throw new DomainException($"Ya existe otro cliente con el número de teléfono '{clientUpdateDto.PhoneNumber}'.", code: "CLIENT_PHONE_ALREADY_EXISTS");
                }
            }

            existClient.FirstName = clientUpdateDto.FirstName;
            existClient.LastName = clientUpdateDto.LastName;
            existClient.Email = clientUpdateDto.Email;
            existClient.PhoneNumber = clientUpdateDto.PhoneNumber;

            _unitOfWork.Clients.Update(existClient);
            var rowsAffected = await _unitOfWork.Complete();
            return rowsAffected > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var clientToDelete = await _unitOfWork.Clients.GetById(id);
            if (clientToDelete == null)
            {
                return false;
            }

            _unitOfWork.Clients.Delete(clientToDelete);
            var rowsAffected = await _unitOfWork.Complete();
            return rowsAffected > 0;
        }

    }
}