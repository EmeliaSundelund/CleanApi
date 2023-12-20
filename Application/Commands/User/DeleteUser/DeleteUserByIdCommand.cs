using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.Users.DeleteUser
{
    public class DeleteUserByIdCommand : IRequest<bool>
    {
        public DeleteUserByIdCommand(Guid deletedUserId)
        {
            DeletedUserId = deletedUserId;
        }

        public Guid DeletedUserId { get; }
    }
}
