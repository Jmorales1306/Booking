using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookingRepository(StoreContext storeContext) : IBookingRepository
    {
        private readonly StoreContext _context = storeContext;

        public async Task<IEnumerable<Booking>> GetAll()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking?> GetById(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task Add(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
        }
        public void Update(Booking booking)
        {
            _context.Bookings.Attach(booking);
            _context.Entry(booking).State = EntityState.Modified;
        }
        public void Delete(Booking booking)
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