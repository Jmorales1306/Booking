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


        private static readonly System.Text.RegularExpressions.Regex PhoneRegex = new(@"^\+?[1-9]\d{6,19}$", System.Text.RegularExpressions.RegexOptions.Compiled);

        public async Task<ClientDto> GetById(int id)
        {
            var client = await _unitOfWork.Clients.GetById(id) ?? throw new DomainException("CLIENT_NOT_FOUND");
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
                throw new DomainException(
                    "CLIENT_EMAIL_ALREADY_EXISTS",
                    target: "email");
            }
            if (!string.IsNullOrWhiteSpace(clientInsertDto.PhoneNumber))
            {
                if (clientInsertDto.PhoneNumber.Length > 20)
                {
                    throw new DomainException("INVALID_PHONE_LENGTH", target: "phoneNumber");
                }

                if (!PhoneRegex.IsMatch(clientInsertDto.PhoneNumber))
                {
                    throw new DomainException("INVALID_PHONE_FORMAT", target: "phoneNumber");
                }

                var existClientByPhone = await _unitOfWork.Clients.GetByPhoneNumber(clientInsertDto.PhoneNumber);
                if (existClientByPhone != null)
                {
                    throw new DomainException(
                        "CLIENT_PHONE_ALREADY_EXISTS",
                        target: "phoneNumber");
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
                throw new DomainException("CLIENT_NOT_FOUND");
            }
            var clientWithSameEmail = await _unitOfWork.Clients.GetByEmail(clientUpdateDto.Email);
            if (clientWithSameEmail != null && clientWithSameEmail.Id != clientUpdateDto.Id)
            {
                throw new DomainException(
                    "CLIENT_EMAIL_ALREADY_EXISTS",
                    target: "email");
            }

            if (!string.IsNullOrWhiteSpace(clientUpdateDto.PhoneNumber))
            {
                if (clientUpdateDto.PhoneNumber.Length > 20)
                {
                    throw new DomainException("INVALID_PHONE_LENGTH", target: "phoneNumber");
                }

                if (!PhoneRegex.IsMatch(clientUpdateDto.PhoneNumber))
                {
                    throw new DomainException("INVALID_PHONE_FORMAT", target: "phoneNumber");
                }

                var clientWithSamePhone = await _unitOfWork.Clients.GetByPhoneNumber(clientUpdateDto.PhoneNumber);
                if (clientWithSamePhone != null && clientWithSamePhone.Id != clientUpdateDto.Id)
                {
                    throw new DomainException(
                        "CLIENT_PHONE_ALREADY_EXISTS",
                        target: "phoneNumber");
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
                throw new DomainException("CLIENT_NOT_FOUND");
            }

            _unitOfWork.Clients.Delete(clientToDelete);
            var rowsAffected = await _unitOfWork.Complete();
            return rowsAffected > 0;
        }

    }
}