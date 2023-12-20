using Domain.Models.AnimalUser;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.AnimalUser.DeleteAnimalUser
{
    public class DeleteAnimalUserCommandHandler : IRequestHandler<DeleteAnimalUserCommand, bool>
    {
        private readonly IAnimalUserRepository _animalUserRepository;

        public DeleteAnimalUserCommandHandler(IAnimalUserRepository animalUserRepository)
        {
            _animalUserRepository = animalUserRepository;
        }

        public async Task<bool> Handle(DeleteAnimalUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine($"Handling DeleteAnimalUserCommand for AnimalUserId: {request.DeletedAnimalUser}");

                AnimalUserModel animalUserToDelete = await _animalUserRepository.GetByKeyAsync(request.DeletedAnimalUser);

                if (animalUserToDelete == null)
                {
                    Console.WriteLine($"AnimalUser with ID {request.DeletedAnimalUser} not found.");
                    return false;
                }

                await _animalUserRepository.DeleteAsync(request.DeletedAnimalUser);
                Console.WriteLine($"Deleted AnimalUser with ID {request.DeletedAnimalUser}.");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling DeleteAnimalUserCommand: {ex.Message}");
                throw;
            }
        }
    }
}