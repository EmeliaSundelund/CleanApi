using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogByIdCommand : IRequest<bool>
    {
        public DeleteDogByIdCommand(Guid deletedDogId)
        {
            DeletedDogId = deletedDogId;
        }

        public Guid DeletedDogId { get; }
    }
}
