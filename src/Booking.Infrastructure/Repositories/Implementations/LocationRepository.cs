using Booking.Core.Entities;

namespace Booking.Infrastructure.Repositories.Implementations
{
    public class LocationRepository(AppDbContext appDbContext) : ILocationRepository
    {
        private readonly AppDbContext _context = appDbContext;
        public async Task<IEnumerable<Location>> GetAll()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task<Location?> GetById(int id)
        {
            return await _context.Locations.FindAsync(id);
        }

        public async Task Add(Location location)
        {
            await _context.Locations.AddAsync(location);
        }

        public void Update(Location location)
        {
            _context.Locations.Attach(location);
            _context.Entry(location).State = EntityState.Modified;
        }

        public void Delete(Location location)
        {
            _context.Locations.Remove(location);
        }

        public async Task<Location?> GetByName(string name)
        {
            return await _context.Locations.FirstOrDefaultAsync(l => l.Name == name);
        }
    }
}