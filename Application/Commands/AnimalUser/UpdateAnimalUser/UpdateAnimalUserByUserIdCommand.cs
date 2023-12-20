using MediatR;

namespace Application.Commands.AnimalUser.UpdateAnimalUser
{
    public class UpdateAnimalUserByUserIdCommand : IRequest<bool>
    {
        public Guid AnimalId { get; }
        public Guid OldUserId { get; }

        public UpdateAnimalUserByUserIdCommand(Guid animalId, Guid oldUserId)
        {
            AnimalId = animalId;
            OldUserId = oldUserId;
        }
    }
}