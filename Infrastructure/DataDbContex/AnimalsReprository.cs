using Domain.Models;
using Domain.Models.Animal;
using Domain.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataDbContex
{
    public class AnimalsRepository : IAnimalsRepository
    {
        private readonly DataDbContex _context;

        public AnimalsRepository(DataDbContex context)
        {
            _context = context;
        }

        public async Task<AnimalModel> GetByIdAsync(Guid Animalid)
        {
            return await _context.Animals.FindAsync(Animalid);
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AnimalModel animal)
        {
            
            _context.Animals.Update(animal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var animalToDelete = await _context.Animals.FindAsync(id);
            if (animalToDelete != null)
            {
                _context.Animals.Remove(animalToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Bird>> GetAllBirdsAsync()
        {
            return await _context.Birds.ToListAsync();
        }

        public async Task<List<Cat>> GetAllCatsAsync()
        {
            return await _context.Cats.ToListAsync();
        }

        public async Task<List<Dog>> GetAllDogsAsync()
        {
            return await _context.Dogs.ToListAsync();
        }
    }
}
