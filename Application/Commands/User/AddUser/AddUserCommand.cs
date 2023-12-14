using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.User.AddUser
{
    public class AddUserCommand : IRequest<UserS>
    {
        public AddUserCommand(UserDto newUser)
        {
            NewUser = newUser;
        }

        public UserDto NewUser { get; }
    }
}