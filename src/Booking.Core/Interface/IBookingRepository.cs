namespace Booking.Core.Interface
{

    public interface IBookingRepository : IRepository<Entities.Booking>
    {
        Task<Entities.Booking?> GetByReference(string reference);
    }
}