using Domain.Models;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Dogs.UpdateDog
{
    public class UpdateDogByIdCommandHandler : IRequestHandler<UpdateDogByIdCommand, Dog>
    {
        private readonly IAnimalsRepository _animalRepository;
        private readonly ILogger<UpdateDogByIdCommandHandler> _logger;

        public UpdateDogByIdCommandHandler(IAnimalsRepository animalRepository, ILogger<UpdateDogByIdCommandHandler> logger)
        {
            _animalRepository = animalRepository;
            _logger = logger;
        }

        public async Task<Dog> Handle(UpdateDogByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling UpdateDogByIdCommand for Dog ID: {request.AnimalId}");

                var dogToUpdate = await _animalRepository.GetByIdAsync(request.AnimalId) as Dog;

                if (dogToUpdate != null)
                {
                    dogToUpdate.Name = request.UpdatedDog.Name;
                    dogToUpdate.BreedDog = request.UpdatedDog.BreedDog;
                    dogToUpdate.WeightDog = request.UpdatedDog.WeightDog;

                    await _animalRepository.UpdateAsync(dogToUpdate);

                    _logger.LogInformation($"Updated Dog with ID: {request.AnimalId}");

                    return dogToUpdate;
                }
                else
                {
                    _logger.LogError($"Dog with ID {request.AnimalId} not found.");
                    throw new InvalidOperationException($"Dog with ID {request.AnimalId} not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error handling UpdateDogByIdCommand: {ex.Message}");
                throw;
            }
        }
    }
}