using MediatR;

namespace Application.Commands.AnimalUser.UpdateAnimalUser
{
    public class UpdateAnimalUserByUserIdCommand : IRequest<bool>
    {
        public Guid AnimalId { get; }
        public Guid NewUserId { get; }

        public UpdateAnimalUserByUserIdCommand(Guid animalId, Guid newUserId)
        {
            AnimalId = animalId;
            NewUserId = newUserId;
        }
    }
}