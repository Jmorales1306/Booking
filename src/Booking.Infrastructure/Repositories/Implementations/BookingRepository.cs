namespace Booking.Infrastructure.Repositories.Implementations
{
    public class BookingRepository(AppDbContext appDbContext) : IBookingRepository
    {
        private readonly AppDbContext _context = appDbContext;

        public async Task<IEnumerable<Reservation>> GetAll()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Reservation?> GetById(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task Add(Reservation booking)
        {
            await _context.Bookings.AddAsync(booking);
        }
        public void Update(Reservation booking)
        {
            _context.Bookings.Attach(booking);
            _context.Entry(booking).State = EntityState.Modified;
        }
        public void Delete(Reservation booking)
        {
            _context.Bookings.Remove(booking);
        }

        public async Task<bool> IsRoomAvailable(int roomId, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            var hasOverlap = await _context.Bookings
               .AnyAsync(b =>
                   b.RoomId == roomId &&
                   b.Date.Date == date.Date &&
                   startTime < b.EndTime &&
                   endTime > b.StartTime
               );

            return !hasOverlap;
        }

        public async Task<bool> IsRoomAvailableForUpdate(int roomId, DateTime date, TimeSpan startTime, TimeSpan endTime, int bookingIdToExclude)
        {
            var hasOverlap = await _context.Bookings
                .AnyAsync(b =>
                    b.Id != bookingIdToExclude &&
                    b.RoomId == roomId &&
                    b.Date.Date == date.Date &&
                    startTime < b.EndTime &&
                    endTime > b.StartTime
                );

            return !hasOverlap;
        }
    }
}