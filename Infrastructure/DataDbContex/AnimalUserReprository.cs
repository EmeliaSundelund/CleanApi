using Domain.Models;
using Domain.Models.AnimalUser;
using Infrastructure.DataDbContex;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.DataDbContex
{
    public class AnimalUserRepository : IAnimalUserRepository
    {
        private readonly DataDbContex _context;
      

        public AnimalUserRepository(DataDbContex context)
        {
            _context = context;
 
        }

        public async Task<bool> AddUserAnimalAsync(AnimalUserModel animalUser)
        {


            await _context.AnimalUser.AddAsync(animalUser);
            await _context.SaveChangesAsync();

            return true;

        }
        
    }
}