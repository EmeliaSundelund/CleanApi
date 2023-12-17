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
            return await _context.Person.FindAsync(userid);
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserModel Person)
        {
            _context.Person.Update(Person);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid userId)
        {
            var userToDelete = await _context.Person.FindAsync(userId);
            if (userToDelete != null)
            {
                _context.Person.Remove(userToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await _context.Person.ToListAsync();
        }
    }
}
