using Application.Commands.Cats.AddCat;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Cats.DeleteCat
{
    public class DeleteCatByIdCommand : IRequest<bool>
    {
        public DeleteCatByIdCommand(CatDto deletedCat, Guid deletedCatId)
        {
            DeletedCat = deletedCat;
            DeletedCatId = deletedCatId;
        }

        public CatDto DeletedCat { get; }
        public Guid DeletedCatId { get; }
    }
}
