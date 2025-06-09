using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreContext _context;
        public UserRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public void Update(User user)
        {
            _context.Users.Attach(user);
            _context.Entry(user).State = EntityState.Modified;
        }

        public void Delete(User user)
        => _context.Users.Remove(user);

    }
}