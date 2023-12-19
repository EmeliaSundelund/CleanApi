using FluentValidation;
using MediatR;

namespace Application.Commands.AnimalUser.DeleteAnimalUser
{
    public class DeleteAnimalUserCommand : IRequest<bool>
    {
        public DeleteAnimalUserCommand(Guid deletedAnimalUser)
        {
            DeletedAnimalUser = deletedAnimalUser;
        }

        public Guid DeletedAnimalUser { get; }

    }
}