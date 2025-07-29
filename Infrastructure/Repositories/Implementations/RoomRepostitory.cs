using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class RoomRepostitory(StoreContext storeContext) : IRoomRepository
    {
        private readonly StoreContext _context = storeContext;

        public async Task<IEnumerable<Room>> GetAll()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room?> GetById(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task Add(Room room)
        {
            await _context.Rooms.AddAsync(room);
        }
        public void Update(Room room)
        {
            _context.Rooms.Attach(room);
            _context.Entry(room).State = EntityState.Modified;
        }
        public void Delete(Room room)
        {
            _context.Rooms.Remove(room);
        }

        public async Task<Room?> GetByName(string name)
        {
            return await _context.Rooms.FirstOrDefaultAsync(r => r.Name == name);
        }
    }
}