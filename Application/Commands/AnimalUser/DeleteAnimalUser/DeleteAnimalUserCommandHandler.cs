using System;
using Domain.Models.AnimalUser;
using Infrastructure.DataDbContex;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;

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
            AnimalUserModel animalUserToDelete = await _animalUserRepository.GetByKeyAsync(request.DeletedAnimalUser);

            if (animalUserToDelete == null)
            {
                return false;
            }

            await _animalUserRepository.DeleteAsync(request.DeletedAnimalUser);
            return true;
        }
    }
}