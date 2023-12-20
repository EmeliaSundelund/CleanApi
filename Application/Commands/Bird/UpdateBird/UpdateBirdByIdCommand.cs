using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Birds.UpdateBird
{
    public class UpdateBirdByIdCommand : IRequest<Bird>
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