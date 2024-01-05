using Domain.Models.Person;
using MediatR;
using Application.Commands.Users.DeleteUser;
using Infrastructure.DataDbContex.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Commands.User.DeleteUser.DeleteUserByIdCommandHandler
{
    public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, bool>
    {
        private readonly IUserInterface _userInterface;
        private readonly ILogger<DeleteUserByIdCommandHandler> _logger;

        public DeleteUserByIdCommandHandler(IUserInterface userInterface, ILogger<DeleteUserByIdCommandHandler> logger)
        {
            _userInterface = userInterface;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling DeleteUserByIdCommand for User ID: {request.DeletedUserId}");

                UserModel userToDelete = await _userInterface.GetByIdAsync(request.DeletedUserId);

                if (userToDelete == null)
                {
                    _logger.LogWarning($"User with ID {request.DeletedUserId} not found.");
                    return false;
                }

                await _userInterface.DeleteAsync(request.DeletedUserId);
                _logger.LogInformation($"Deleted User with ID: {request.DeletedUserId}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error handling DeleteUserByIdCommand: {ex.Message}");
                throw;
            }
        }
    }
}