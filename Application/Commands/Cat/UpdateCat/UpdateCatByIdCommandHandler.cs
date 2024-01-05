using Domain.Models;
using MediatR;
using Infrastructure.DataDbContex.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Cats.UpdateCat
{
    public class UpdateCatByIdCommandHandler : IRequestHandler<UpdateCatByIdCommand, Cat>
    {
        private readonly IAnimalsRepository _animalRepository;
        private readonly ILogger<UpdateCatByIdCommandHandler> _logger;

        public UpdateCatByIdCommandHandler(IAnimalsRepository animalRepository, ILogger<UpdateCatByIdCommandHandler> logger)
        {
            _animalRepository = animalRepository;
            _logger = logger;
        }

        public async Task<Cat> Handle(UpdateCatByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling UpdateCatByIdCommand for Cat ID: {request.AnimalId}");

                var catToUpdate = await _animalRepository.GetByIdAsync(request.AnimalId) as Cat;

                if (catToUpdate != null)
                {
                    catToUpdate.Name = request.UpdatedCat.Name;
                    catToUpdate.BreedCat = request.UpdatedCat.BreedCat;
                    catToUpdate.WeightCat = request.UpdatedCat.WeightCat;

                    await _animalRepository.UpdateAsync(catToUpdate);

                    _logger.LogInformation($"Updated Cat with ID: {request.AnimalId}");

                    return catToUpdate;
                }
                else
                {
                    _logger.LogError($"Cat with ID {request.AnimalId} not found.");
                    throw new InvalidOperationException($"Cat with ID {request.AnimalId} not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error handling UpdateCatByIdCommand: {ex.Message}");
                throw;
            }
        }
    }
}