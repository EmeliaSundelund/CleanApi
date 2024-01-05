using Domain.Models.Person;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.User.UpdateUser
{
    public class UpdateUserByIdCommandHandler : IRequestHandler<UpdateUserByIdCommand, UserModel>
    {
        private readonly IUserInterface _userInterface;
        private readonly ILogger<UpdateUserByIdCommandHandler> _logger;

        public UpdateUserByIdCommandHandler(IUserInterface userInterface, ILogger<UpdateUserByIdCommandHandler> logger)
        {
            _userInterface = userInterface;
            _logger = logger;
        }

        public async Task<UserModel> Handle(UpdateUserByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling UpdateUserByIdCommand for User ID: {request.UserId}");

                var userToUpdate = await _userInterface.GetByIdAsync(request.UserId) as UserModel;

                if (userToUpdate != null)
                {
                    userToUpdate.UserName = request.UpdatedUser.UserName;
                    userToUpdate.Password = request.UpdatedUser.Password;

                    await _userInterface.UpdateAsync(userToUpdate);

                    _logger.LogInformation($"Updated User with ID: {request.UserId}");

                    return userToUpdate;
                }
                else
                {
                    _logger.LogError($"User with ID {request.UserId} not found.");
                    throw new InvalidOperationException($"User with ID {request.UserId} not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error handling UpdateUserByIdCommand: {ex.Message}");
                throw;
            }
        }
    }
}