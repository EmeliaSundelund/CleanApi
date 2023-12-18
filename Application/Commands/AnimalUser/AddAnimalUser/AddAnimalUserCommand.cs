using Application.Dtos;
using FluentValidation;
using MediatR;

namespace Application.Commands.AnimalUser.AddAnimalUser
{
    public class AddAnimalUserCommand : IRequest<bool>
    {
        public AddAnimalUserCommand(AnimalUserDto newAnimalUser)
        {
            NewAnimalUser = newAnimalUser;
        }

        public AnimalUserDto NewAnimalUser { get; }

       
    }
    
}