using Domain.Models;
using Domain.Models.Person;
using Infrastructure.Database;
using Infrastructure.DataDbContex;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Commands.User.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserModel>
    {
        private readonly IConfiguration _configuration;

        private readonly Infrastructure.DataDbContex.DataDbContex _dataDbContex;

        public AddUserCommandHandler(IConfiguration configuration, Infrastructure.DataDbContex.DataDbContex dataDbContex)
        {
            _configuration = configuration;
            _dataDbContex = dataDbContex;
        }

        public async Task<UserModel> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            UserModel userToCreate = new()
            {
                UserId = Guid.NewGuid(),
                UserName = request.NewUser.UserName,
                Password = request.NewUser.Password,


            };

            await _dataDbContex.Person.AddAsync(userToCreate);
            await _dataDbContex.SaveChangesAsync();

            return userToCreate;
        }
    }
}
