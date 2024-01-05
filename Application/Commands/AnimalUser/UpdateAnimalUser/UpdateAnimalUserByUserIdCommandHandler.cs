using Domain.Models.AnimalUser;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.AnimalUser.UpdateAnimalUser
{
    public class UpdateAnimalUserByUserIdCommandHandler : IRequestHandler<UpdateAnimalUserByUserIdCommand, bool>
    {
        private readonly IAnimalUserRepository _animalUserRepository;
        private readonly ILogger<UpdateAnimalUserByUserIdCommandHandler> _logger;

        public UpdateAnimalUserByUserIdCommandHandler(IAnimalUserRepository animalUserRepository, ILogger<UpdateAnimalUserByUserIdCommandHandler> logger)
        {
            _animalUserRepository = animalUserRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateAnimalUserByUserIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newAnimalUser = new AnimalUserModel
                {
                    AnimalId = request.AnimalId,
                    UserId = request.OldUserId
                };

                await _animalUserRepository.AddUserAnimalAsync(newAnimalUser);

                _logger.LogInformation("AnimalUser updated successfully.");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error handling UpdateAnimalUserByUserIdCommand: {ex.Message}");
                throw;
            }
        }
    }
}