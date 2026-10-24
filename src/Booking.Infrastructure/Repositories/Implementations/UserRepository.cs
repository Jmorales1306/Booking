using Booking.Core.Entities;

namespace Booking.Infrastructure.Repositories.Implementations
{
    public class UserRepository(AppDbContext appDbContext) : IUserRepository
    {
        private readonly AppDbContext _context = appDbContext;

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
        }
        public void Update(User user)
        {
            _context.Users.Attach(user);
            _context.Entry(user).State = EntityState.Modified;
        }
        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }
    }
}