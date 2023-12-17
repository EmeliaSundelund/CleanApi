using System;
using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.User.UpdateUser
{
    public class UpdateUserByIdCommand : IRequest<Users>
    {
        public UpdateUserByIdCommand(UserDto updatedUser, Guid id)
        {
            UpdatedUser = updatedUser;
            Id = id;
        }

        public UserDto UpdatedUser { get; }
        public Guid Id { get; }

    }
}