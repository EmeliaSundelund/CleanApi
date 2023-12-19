using Infrastructure.DataDbContex;
using Domain.Models.Person;
using MediatR;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Application.Commands.Users.DeleteUser;
using Infrastructure.DataDbContex.Interfaces;

namespace Application.Commands.User.DeleteUser.DeleteUserByIdCommandHandler
{
    public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, bool>
    {
        private readonly IUserInterface _userInterface;

        public DeleteUserByIdCommandHandler(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public async Task<bool> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            UserModel userToDelete = await _userInterface.GetByIdAsync(request.DeletedUserId);

            if (userToDelete == null)
            {
                return false;
            }

            await _userInterface.DeleteAsync(request.DeletedUserId);
            return true;
        }
    }
}
