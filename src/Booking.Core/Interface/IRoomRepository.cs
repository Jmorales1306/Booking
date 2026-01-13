namespace Booking.Core.Interface
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<Room?> GetByName(string name);
    }
}