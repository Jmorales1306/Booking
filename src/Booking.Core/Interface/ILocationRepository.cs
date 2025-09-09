namespace Booking.Core.Interface
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<Location?> GetByName(string name);
    }
}