using Domain.Models;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging; 
namespace Application.Commands.Dogs.UpdateDog
{
    public class UpdateDogByIdCommandHandler : IRequestHandler<UpdateDogByIdCommand, Dog>
    {
        private readonly IAnimalsRepository _animalRepository;

        public UpdateDogByIdCommandHandler(IAnimalsRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<Dog> Handle(UpdateDogByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine("Handling UpdateDogByIdCommand for Dog ID: {request.AnimalId}");

                var dogToUpdate = await _animalRepository.GetByIdAsync(request.AnimalId) as Dog;

                if (dogToUpdate != null)
                {
                    dogToUpdate.Name = request.UpdatedDog.Name;
                    dogToUpdate.BreedDog = request.UpdatedDog.BreedDog;
                    dogToUpdate.WeightDog = request.UpdatedDog.WeightDog;

                    await _animalRepository.UpdateAsync(dogToUpdate);

                    Console.WriteLine("Updated Dog with ID: {request.AnimalId}");

                    return dogToUpdate;
                }
                else
                {
                    Console.WriteLine("Dog with ID {request.AnimalId} not found.");
                    throw new InvalidOperationException($"Dog with ID {request.AnimalId} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error handling UpdateDogByIdCommand: {ex.Message}");
                throw;
            }
        }
    }
}