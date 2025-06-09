using Domain.Models;

namespace Domain.Repositories
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetBookingsByClientId(int clientId);
        Task<IEnumerable<Booking>> GetBookingsByDateRange(DateTime startDate, DateTime endDate);
    }
}
