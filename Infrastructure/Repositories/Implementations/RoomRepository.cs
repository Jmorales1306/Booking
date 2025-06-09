using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class RoomRepository : IRoomRepository
    {
        private readonly StoreContext _context;

        public RoomRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAll()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetById(int id)
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
            _context.Rooms.Entry(room).State = EntityState.Modified;
        }

        public void Delete(Room room)
        => _context.Rooms.Remove(room);

    }
}