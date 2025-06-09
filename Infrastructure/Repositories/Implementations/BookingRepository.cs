using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class BookingRepository : IBookingRepository
    {
        private readonly StoreContext _context;

        public BookingRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            return await _context.Bookings.Include(b => b.Client)
                        .Include(b => b.Room)
                        .ToListAsync();
        }

        public async Task<Booking> GetById(int clientId)
        {
            return await _context.Bookings
            .Include(b => b.Client)
            .Include(b => b.Room)
            .Include(b => b.User)
            .Where(b => b.ClientId == clientId)
            .FirstAsync();

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
        => _context.Bookings.Remove(booking);

        public async Task<IEnumerable<Booking>> GetBookingsByClientId(int clientId)
        {
            return await _context.Bookings
            .Include(b => b.ClientId)
            .Include(b => b.Room)
            .Include(b => b.ClientId == clientId)
            .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.Bookings
                        .Include(b => b.ClientId)
                        .Include(b => b.Room)
                        .Where(b => b.Date >= startDate.Date && b.Date <= endDate.Date)
                        .ToListAsync();
        }
    }
}