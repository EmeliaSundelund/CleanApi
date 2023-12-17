using Application.Dtos;
using Domain.Models;
using Domain.Models.Person;
using MediatR;

namespace Application.Commands.User.AddUser
{
    public class AddUserCommand : IRequest<UserModel>
    {
        public AddUserCommand(UserDto newUser)
        {
            NewUser = newUser;
        }

        public UserDto NewUser { get; }
    }
}