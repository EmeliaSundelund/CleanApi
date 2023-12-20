using Domain.Models;
using Domain.Models.Animal;

namespace Infrastructure.DataDbContex.Interfaces
{
    public interface IAnimalsRepository
    {
        Task<AnimalModel> GetByIdAsync(Guid animalId);
        Task AddAsync<T>(T entity) where T : class;
        Task UpdateAsync(AnimalModel animal);
        Task DeleteAsync(Guid animalId);
        Task<List<Bird>> GetAllBirdsAsync();
        Task<List<Cat>> GetAllCatsAsync();
        Task<List<Dog>> GetAllDogsAsync();
        Task<List<Bird>> GetBirdsByColorAsync(string color);
        Task<List<Dog>> GetDogsByBreedAsync(string breedDog);
        Task<List<Dog>> GetDogsByWeightAsync(int weightDog);
        Task<List<Cat>> GetCatsByBreedAsync(string breedCat);
        Task<List<Cat>> GetCatsByWeightAsync(int weightCat);
    }
}

