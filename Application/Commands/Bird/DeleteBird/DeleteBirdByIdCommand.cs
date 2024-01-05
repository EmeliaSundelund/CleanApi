using MediatR;

namespace Application.Commands.Bird.DeleteBird
{
    public class DeleteBirdByIdCommand : IRequest<bool>
    {
        public DeleteBirdByIdCommand(Guid deletedBirdId)
        {
            DeletedBirdId = deletedBirdId;
        }

        public Guid DeletedBirdId { get; }
    }
}