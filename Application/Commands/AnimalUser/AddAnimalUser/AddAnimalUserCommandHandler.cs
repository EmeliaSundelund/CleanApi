using Application.Commands.AnimalUser.AddAnimalUser;
using Domain.Models.AnimalUser;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;

namespace Application.AnimalUsers.Commands.AddAnimalUser
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
            AnimalUserModel userAnimal = new()
            {
                UserId = request.NewAnimalUser.UserId,
                AnimalId = request.NewAnimalUser.AnimalId,

            };

            return await _animalUserRepository.AddUserAnimalAsync(userAnimal);
        }
    }
}