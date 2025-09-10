namespace Booking.Core.Interface
{

    public interface IBookingRepository : IRepository<Entities.Booking>
    {
        Task<bool> IsRoomAvailable(int roomId, DateTime date, TimeSpan startTime, TimeSpan endTime);
        Task<bool> IsRoomAvailableForUpdate(int roomId, DateTime date, TimeSpan startTime, TimeSpan endTime, int bookingIdToExclude);
        
    }
}