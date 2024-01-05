using Domain.Models.Animal;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Bird.DeleteBird
{
    public class DeleteBirdByIdCommandHandler : IRequestHandler<DeleteBirdByIdCommand, bool>
    {
        private readonly IAnimalsRepository _animalsRepository;
        private readonly ILogger<DeleteBirdByIdCommandHandler> _logger;

        public DeleteBirdByIdCommandHandler(IAnimalsRepository animalsRepository, ILogger<DeleteBirdByIdCommandHandler> logger)
        {
            _animalsRepository = animalsRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteBirdByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                AnimalModel birdToDelete = await _animalsRepository.GetByIdAsync(request.DeletedBirdId);

                if (birdToDelete == null)
                {
                    _logger.LogWarning($"Bird with ID {request.DeletedBirdId} not found.");
                    return false;
                }

                await _animalsRepository.DeleteAsync(request.DeletedBirdId);
                _logger.LogInformation($"Deleted Bird with ID {request.DeletedBirdId}.");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error handling DeleteBirdByIdCommand: {ex.Message}");
                throw;
            }
        }
    }
}