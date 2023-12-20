using Domain.Models.Person;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;

namespace Application.Commands.User.UpdateUser
{
    public class UpdateUserByIdCommandHandler : IRequestHandler<UpdateUserByIdCommand, UserModel>
    {
        private readonly IUserInterface _userInterface;

        public UpdateUserByIdCommandHandler(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public async Task<UserModel> Handle(UpdateUserByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine("Handling UpdateUserByIdCommand for User ID: {request.UserId}");

                var userToUpdate = await _userInterface.GetByIdAsync(request.UserId) as UserModel;

                if (userToUpdate != null)
                {
                    userToUpdate.UserName = request.UpdatedUser.UserName;
                    userToUpdate.Password = request.UpdatedUser.Password;

                    await _userInterface.UpdateAsync(userToUpdate);

                    Console.WriteLine("Updated User with ID: {request.UserId}");

                    return userToUpdate;
                }
                else
                {
                    Console.WriteLine("User with ID {request.UserId} not found.");
                    throw new InvalidOperationException($"User with ID {request.UserId} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error handling UpdateUserByIdCommand: {ex.Message}");
                throw;
            }
        }
    }
}
