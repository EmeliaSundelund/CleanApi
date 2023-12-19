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
            Console.WriteLine($"Handling UpdateAnimalUserByUserIdCommand for AnimalId: {request.AnimalId}");

            try
            {
                // Skapa en ny AnimalUserModel med det nya AnimalId och UserId
                var newAnimalUser = new AnimalUserModel
                {
                    AnimalId = request.AnimalId,
                    UserId = request.NewUserId
                    // Lägg till andra egenskaper beroende på din modell
                };

                // Lägg till den nya raden i AnimalUser
                await _animalUserRepository.AddUserAnimalAsync(newAnimalUser);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling UpdateAnimalUserByUserIdCommand: {ex.Message}");
                throw;
            }
        }


    }

}
