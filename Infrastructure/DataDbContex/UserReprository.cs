using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Person;

namespace Infrastructure.DataDbContex
{
    public class UsersRepository : UserInterface
    {
        private readonly DataDbContex _context;

        public UsersRepository(DataDbContex context)
        {
            _context = context;
        }

        public async Task<UserModel> GetByIdAsync(Guid userid)
        {
            return await _context.Users.FindAsync(userid);
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserModel user)
        {
            _context.Users.Update((UserS)user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid userId)
        {
            var userToDelete = await _context.Users.FindAsync(userId);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<UserS>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
