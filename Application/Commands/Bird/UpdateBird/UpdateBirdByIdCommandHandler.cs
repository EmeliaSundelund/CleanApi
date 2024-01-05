using Domain.Models;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Bird.UpdateBird
{
    public class UpdateBirdByIdCommandHandler : IRequestHandler<UpdateBirdByIdCommand, Domain.Models.Bird>
    {
        private readonly IAnimalsRepository _animalRepository;
        private readonly ILogger<UpdateBirdByIdCommandHandler> _logger;

        public UpdateBirdByIdCommandHandler(IAnimalsRepository animalRepository, ILogger<UpdateBirdByIdCommandHandler> logger)
        {
            _animalRepository = animalRepository;
            _logger = logger;
        }

        public async Task<Domain.Models.Bird> Handle(UpdateBirdByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling UpdateBirdByIdCommand for Bird ID: {request.AnimalId}");

                var birdToUpdate = await _animalRepository.GetByIdAsync(request.AnimalId) as Domain.Models.Bird;

                if (birdToUpdate != null)
                {
                    birdToUpdate.Name = request.UpdatedBird.Name;
                    birdToUpdate.Color = request.UpdatedBird.Color;

                    await _animalRepository.UpdateAsync(birdToUpdate);

                    _logger.LogInformation($"Updated Bird with ID: {request.AnimalId}");

                    return birdToUpdate;
                }
                else
                {
                    _logger.LogError($"Bird with ID {request.AnimalId} not found.");
                    throw new InvalidOperationException($"Bird with ID {request.AnimalId} not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error handling UpdateBirdByIdCommand: {ex.Message}");
                throw;
            }
        }
    }
}