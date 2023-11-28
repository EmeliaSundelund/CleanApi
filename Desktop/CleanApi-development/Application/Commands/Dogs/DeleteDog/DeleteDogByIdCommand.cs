using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogByIdCommand : IRequest<bool>
    {
        public DeleteDogByIdCommand(DogDto deletedDog, Guid deletedDogId)
        {
            DeletedDog = deletedDog;
            DeletedDogId = deletedDogId;
        }

        public DogDto DeletedDog { get; }
        public Guid DeletedDogId { get; }
    }
}

