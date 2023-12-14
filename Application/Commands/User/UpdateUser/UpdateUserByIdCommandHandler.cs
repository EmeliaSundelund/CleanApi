using Domain.Models;
using Infrastructure.DataDbContex;
using MediatR;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Application.Commands.User.UpdateUser
{
    public class UpdateUserByIdCommandHandler : IRequestHandler<UpdateUserByIdCommand, UserS>
    {
        private readonly UserInterface _userInterface;

        public UpdateUserByIdCommandHandler(UserInterface userInterface)
        {
            _userInterface = userInterface;
        }
        public async Task<UserS> Handle(UpdateUserByIdCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _userInterface.GetByIdAsync(request.Id) as UserS;

            if (userToUpdate != null)
            {
                userToUpdate.UserName = request.UpdatedUser.UserName;
                userToUpdate.Password = request.UpdatedUser.Password;
                userToUpdate.Animals = request.UpdatedUser.Animals;
               

                await _userInterface.UpdateAsync(userToUpdate);

                return userToUpdate;
            }
            else
            {
                throw new InvalidOperationException($"Dog with ID {request.Id} not found.");
            }
        }
    }

}