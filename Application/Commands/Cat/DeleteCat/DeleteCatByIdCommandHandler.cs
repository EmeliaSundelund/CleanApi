using Domain.Models.Animal;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Cats.DeleteCat
{
    public class DeleteCatByIdCommandHandler : IRequestHandler<DeleteCatByIdCommand, bool>
    {
        private readonly IAnimalsRepository _animalsRepository;
        private readonly ILogger<DeleteCatByIdCommandHandler> _logger;

        public DeleteCatByIdCommandHandler(IAnimalsRepository animalsRepository, ILogger<DeleteCatByIdCommandHandler> logger)
        {
            _animalsRepository = animalsRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteCatByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling DeleteCatByIdCommand for Cat ID: {request.DeletedCatId}");

                AnimalModel catToDelete = await _animalsRepository.GetByIdAsync(request.DeletedCatId);

                if (catToDelete == null)
                {
                    _logger.LogWarning($"Cat with ID {request.DeletedCatId} not found.");
                    return false;
                }

                await _animalsRepository.DeleteAsync(request.DeletedCatId);
                _logger.LogInformation($"Deleted Cat with ID {request.DeletedCatId}.");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error handling DeleteCatByIdCommand: {ex.Message}");
                throw;
            }
        }
    }
}