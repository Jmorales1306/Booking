using Domain.Models;

namespace Domain.Repositories
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<bool> IsRoomAvailable(int roomId, DateTime date, TimeSpan startTime, TimeSpan endTime);
        Task<bool> IsRoomAvailableForUpdate(int roomId, DateTime date, TimeSpan startTime, TimeSpan endTime, int bookingIdToExclude);

    }
}