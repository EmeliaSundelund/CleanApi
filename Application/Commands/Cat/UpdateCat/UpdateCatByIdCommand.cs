using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Cats.UpdateCat
{
    public class UpdateCatByIdCommand : IRequest<Cat>
    {
        public UpdateCatByIdCommand(CatDto updatedCat, Guid animalId)
        {
            UpdatedCat = updatedCat;
            AnimalId = animalId;
        }

        public CatDto UpdatedCat { get; }
        public Guid AnimalId { get; }
    }
}