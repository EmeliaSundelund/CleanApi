using Infrastructure.DataDbContex;
using Domain.Models.Person;
using MediatR;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Application.Commands.Users.DeleteUser;

namespace Application.Commands.User.DeleteUser.DeleteUserByIdCommandHandler
{
    public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, bool>
    {
        private readonly UserInterface _userInterface;

        public DeleteUserByIdCommandHandler(UserInterface userInterface)
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
