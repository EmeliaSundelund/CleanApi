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
            var userToUpdate = await _userInterface.GetByIdAsync(request.UserId) as UserModel;

            if (userToUpdate != null)
            {
                userToUpdate.UserName = request.UpdatedUser.UserName;
                userToUpdate.Password = request.UpdatedUser.Password;

                await _userInterface.UpdateAsync(userToUpdate);

                return userToUpdate;
            }
            else
            {
                throw new InvalidOperationException($"Dog with ID {request.UserId} not found.");
            }
        }
    }

}