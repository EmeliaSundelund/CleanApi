using Application.Dtos;
using MediatR;

namespace Application.Commands.Birds.DeleteBird
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