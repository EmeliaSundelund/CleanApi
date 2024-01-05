using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Bird.UpdateBird
{
    public class UpdateBirdByIdCommand : IRequest<Domain.Models.Bird>
    {
        public UpdateBirdByIdCommand(BirdDto updatedBird, Guid animalId)
        {
            UpdatedBird = updatedBird;
            AnimalId = animalId;
        }

        public BirdDto UpdatedBird { get; }
        public Guid AnimalId { get; }
    }
}