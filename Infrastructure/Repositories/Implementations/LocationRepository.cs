using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class LocationRepository(StoreContext storeContext) : ILocationRepository
    {
        private readonly StoreContext _context = storeContext;
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