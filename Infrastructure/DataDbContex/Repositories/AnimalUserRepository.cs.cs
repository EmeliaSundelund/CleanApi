using Domain.Models.AnimalUser;
using Infrastructure.DataDbContex.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataDbContex.Repositories
{
    public class AnimalUserRepository : IAnimalUserRepository
    {
        private readonly DataDbContex _context;


        public AnimalUserRepository(DataDbContex context)
        {
            _context = context;

        }

        // Read
        public async Task<List<AnimalUserModel>> GetAllAnimalUsersAsync()
        {

            var animalUsers = await _context.AnimalUser
                    .Select(au => new AnimalUserModel
                    {
                        UserId = au.User.UserId,
                        AnimalId = au.Animal.AnimalId,

                    })
                    .ToListAsync();


            return animalUsers;

        }

        public async Task<bool> AddUserAnimalAsync(AnimalUserModel animalUser)
        {
            await _context.AnimalUser.AddAsync(animalUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AnimalUserModel> GetAnimalUserByIdAsync(Guid animalUserId)
        {

            var animalUser = await _context.AnimalUser.FindAsync(animalUserId);
            return animalUser;

        }



        public async Task<bool> UpdateAnimalUserAsync(AnimalUserModel animalUser)
        {
            try
            {
                var existingAnimalUser = await _context.AnimalUser
                    .FirstOrDefaultAsync(a => a.AnimalUserId == animalUser.AnimalUserId);

                if (existingAnimalUser == null)
                {
                    Console.WriteLine("AnimalUser does not exist in the database.");
                    return false;
                }

                // Uppdatera egenskaper för befintlig AnimalUser med värden från den inkommande modellen
                existingAnimalUser.UserId = animalUser.UserId;
                existingAnimalUser.AnimalId = animalUser.AnimalId;
                // Uppdatera andra egenskaper beroende på din modell

                // Använd Update-metoden för att markera raden som ändrad
                _context.AnimalUser.Update(existingAnimalUser);

                // Spara ändringarna i databasen
                await _context.SaveChangesAsync();

                Console.WriteLine("AnimalUser updated successfully.");
                return true;
            }
            catch (Exception ex)
            {
                // Hantera eventuella undantag och logga felmeddelanden
                Console.WriteLine($"Error updating AnimalUser: {ex.Message}");
                return false;
            }
        }



        public async Task<AnimalUserModel> GetByKeyAsync(Guid key)
        {


            var animalUser = await _context.AnimalUser.FindAsync(key);

            return animalUser;
        }

        public async Task DeleteAsync(Guid key)
        {

            _context.AnimalUser.Remove((AnimalUserModel?)(await _context.AnimalUser.FindAsync(key) ?? throw new Exception("User not found")));
            await _context.SaveChangesAsync();

        }


        public async Task<AnimalUserModel> GetAnimalUserByAnimalIdAsync(Guid animalId)
        {
            try
            {
                var animalUser = await _context.AnimalUser
                    .FirstOrDefaultAsync(a => a.AnimalId == animalId);

                return animalUser;
            }
            catch (Exception ex)
            {
                // Hantera eventuella undantag och logga felmeddelanden
                Console.WriteLine($"Error getting AnimalUser: {ex.Message}");
                throw;
            }
        }

    }
}