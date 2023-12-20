using Application.Dtos;
using Domain.Models.Person;
using MediatR;

namespace Application.Commands.User.UpdateUser
{
    public class UpdateUserByIdCommand : IRequest<UserModel>
    {
        public UpdateUserByIdCommand(UserDto updatedUser, Guid userId)
        {
            UpdatedUser = updatedUser;
            UserId = userId;
        }

        public UserDto UpdatedUser { get; }
        public Guid UserId { get; }

    }
}