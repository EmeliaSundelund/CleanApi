using Domain.Models.AnimalUser;

namespace Infrastructure.DataDbContex
{
    public interface IAnimalUserRepository
    {

        Task<bool> AddUserAnimalAsync(AnimalUserModel animalUser);

    }
}