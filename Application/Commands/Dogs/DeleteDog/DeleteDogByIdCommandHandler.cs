using MediatR;
using Domain.Models.Animal;
using Infrastructure.DataDbContex.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogByIdCommandHandler : IRequestHandler<DeleteDogByIdCommand, bool>
    {
        private readonly IAnimalsRepository _animalsRepository;
        private readonly ILogger<DeleteDogByIdCommandHandler> _logger;

        public DeleteDogByIdCommandHandler(IAnimalsRepository animalsRepository, ILogger<DeleteDogByIdCommandHandler> logger)
        {
            _animalsRepository = animalsRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteDogByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling DeleteDogByIdCommand for Dog ID: {request.DeletedDogId}");

                AnimalModel dogToDelete = await _animalsRepository.GetByIdAsync(request.DeletedDogId);

                if (dogToDelete == null)
                {
                    _logger.LogWarning($"Dog with ID {request.DeletedDogId} not found.");
                    return false;
                }

                await _animalsRepository.DeleteAsync(request.DeletedDogId);
                _logger.LogInformation($"Deleted Dog with ID: {request.DeletedDogId}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error handling DeleteDogByIdCommand: {ex.Message}");
                throw;
            }
        }
    }
}