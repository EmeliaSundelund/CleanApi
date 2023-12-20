using Domain.Models.AnimalUser;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.AnimalUser.UpdateAnimalUser
{
    public class UpdateAnimalUserByUserIdCommandHandler : IRequestHandler<UpdateAnimalUserByUserIdCommand, bool>
    {
        private readonly IAnimalUserRepository _animalUserRepository;

        public UpdateAnimalUserByUserIdCommandHandler(IAnimalUserRepository animalUserRepository)
        {
            _animalUserRepository = animalUserRepository;
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

                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error handling UpdateAnimalUserByUserIdCommand: {ex.Message}");
                // You might want to handle the exception appropriately or propagate it
                throw;
            }
        }
    }
}
