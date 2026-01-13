namespace Booking.Core.Interface
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client?> GetByEmail(string email);
        Task<Client?> GetByPhoneNumber(string phoneNumber);
    }
}