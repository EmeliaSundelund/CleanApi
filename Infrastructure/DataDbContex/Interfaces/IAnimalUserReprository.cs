using Domain.Models.AnimalUser;

namespace Infrastructure.DataDbContex.Interfaces
{
    public interface IAnimalUserRepository
    {
        Task<List<AnimalUserModel>> GetAllAnimalUsersAsync();
        Task<bool> AddUserAnimalAsync(AnimalUserModel animalUser);
        Task<AnimalUserModel> GetAnimalUserByIdAsync(Guid animalUserId);
        Task<bool> UpdateAnimalUserAsync(AnimalUserModel animalUser);
        Task<AnimalUserModel> GetByKeyAsync(Guid key);
        Task DeleteAsync(Guid key);
        Task<AnimalUserModel> GetAnimalUserByAnimalIdAsync(Guid animalId);
    }

}