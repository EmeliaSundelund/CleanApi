using Domain.Models;
using Domain.Models.Person;
using Infrastructure.DataDbContex;
using MediatR;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Application.Commands.User.UpdateUser
{
    public class UpdateUserByIdCommandHandler : IRequestHandler<UpdateUserByIdCommand, UserModel>
    {
        private readonly UserInterface _userInterface;

        public UpdateUserByIdCommandHandler(UserInterface userInterface)
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