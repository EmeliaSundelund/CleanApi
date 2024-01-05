using Domain.Models.AnimalUser;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.AnimalUser.DeleteAnimalUser
{
    public class DeleteAnimalUserCommandHandler : IRequestHandler<DeleteAnimalUserCommand, bool>
    {
        private readonly IAnimalUserRepository _animalUserRepository;
        private readonly ILogger<DeleteAnimalUserCommandHandler> _logger;

        public DeleteAnimalUserCommandHandler(IAnimalUserRepository animalUserRepository, ILogger<DeleteAnimalUserCommandHandler> logger)
        {
            _animalUserRepository = animalUserRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteAnimalUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling DeleteAnimalUserCommand for AnimalUserId: {request.DeletedAnimalUser}");

                AnimalUserModel animalUserToDelete = await _animalUserRepository.GetByKeyAsync(request.DeletedAnimalUser);

                if (animalUserToDelete == null)
                {
                    _logger.LogWarning($"AnimalUser with ID {request.DeletedAnimalUser} not found.");
                    return false;
                }

                await _animalUserRepository.DeleteAsync(request.DeletedAnimalUser);
                _logger.LogInformation($"Deleted AnimalUser with ID {request.DeletedAnimalUser}.");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error handling DeleteAnimalUserCommand: {ex.Message}");
                throw;
            }
        }
    }
}