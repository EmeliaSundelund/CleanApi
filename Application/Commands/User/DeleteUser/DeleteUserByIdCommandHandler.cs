using Domain.Models.Person;
using MediatR;
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
            try
            {
                Console.WriteLine("Handling DeleteUserByIdCommand for User ID: {request.DeletedUserId}");

                UserModel userToDelete = await _userInterface.GetByIdAsync(request.DeletedUserId);

                if (userToDelete == null)
                {
                    Console.WriteLine("User with ID {request.DeletedUserId} not found.");
                    return false;
                }

                await _userInterface.DeleteAsync(request.DeletedUserId);
                Console.WriteLine("Deleted User with ID: {request.DeletedUserId}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error handling DeleteUserByIdCommand: {ex.Message}");
                throw;
            }
        }
    }
}
