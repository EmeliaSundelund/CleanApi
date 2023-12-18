using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Dogs.UpdateDog
{
    public class UpdateDogByIdCommand : IRequest<Dog>
    {
        public UpdateDogByIdCommand(DogDto updatedDog, Guid animalId)
        {
            UpdatedDog = updatedDog;
            AnimalId = animalId;
        }

        public DogDto UpdatedDog { get; }
        public Guid AnimalId { get; }
    }
}
