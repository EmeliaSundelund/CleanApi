using Domain.Models.AnimalUser;
using Infrastructure.DataDbContex;
using MediatR;

namespace Application.Commands.AnimalUser.AddAnimalUser
{
    public class AddAnimalUserCommandHandler : IRequestHandler<AddAnimalUserCommand, bool>
    {
        private readonly IAnimalUserRepository _animalUserRepository;

        public AddAnimalUserCommandHandler(IAnimalUserRepository animalUserRepository)
        {
            _animalUserRepository = animalUserRepository;
        }

        public async Task<bool> Handle(AddAnimalUserCommand request, CancellationToken cancellationToken)
        {
            AnimalUserModel newAnimalUser = new()
            {
                UserId = request.NewAnimalUser.UserId,
                AnimalId = request.NewAnimalUser.AnimalId,
              
            };

            return await _animalUserRepository.AddUserAnimalAsync(newAnimalUser);
        }
    }
    
}