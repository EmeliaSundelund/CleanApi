using Application.Commands.AnimalUser.AddAnimalUser;
using Domain.Models.AnimalUser;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.AnimalUsers.Commands.AddAnimalUser
{
    public class AddAnimalUserCommandHandler : IRequestHandler<AddAnimalUserCommand, bool>
    {
        private readonly IAnimalUserRepository _animalUserRepository;
        private readonly ILogger<AddAnimalUserCommandHandler> _logger;

        public AddAnimalUserCommandHandler(IAnimalUserRepository animalUserRepository, ILogger<AddAnimalUserCommandHandler> logger)
        {
            _animalUserRepository = animalUserRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(AddAnimalUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                AnimalUserModel userAnimal = new()
                {
                    UserId = request.NewAnimalUser.UserId,
                    AnimalId = request.NewAnimalUser.AnimalId,
                };

                return await _animalUserRepository.AddUserAnimalAsync(userAnimal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing AddAnimalUserCommand.");

                throw;
            }
        }
    }
}
