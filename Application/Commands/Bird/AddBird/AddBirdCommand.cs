using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Bird.AddBird
{
    public class AddBirdCommand : IRequest<Domain.Models.Bird>
    {
        public AddBirdCommand(BirdDto newBird)
        {
            NewBird = newBird;
        }

        public BirdDto NewBird { get; }
    }
}