using Domain.Models;
using Infrastructure.Database;
using Infrastructure.DataDbContex;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Commands.User.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserS>
    {
        private readonly IConfiguration _configuration;

        private readonly Infrastructure.DataDbContex.DataDbContex _dataDbContex;

        public AddUserCommandHandler(IConfiguration configuration, Infrastructure.DataDbContex.DataDbContex dataDbContex)
        {
            _configuration = configuration;
            _dataDbContex = dataDbContex;
        }

        public async Task<UserS> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            UserS userToCreate = new()
            {
                Id = Guid.NewGuid(),
                UserName = request.NewUser.UserName,
                Password = request.NewUser.Password,
                Animals = request.NewUser.Animals,

            };

            await _dataDbContex.Users.AddAsync(userToCreate);
            await _dataDbContex.SaveChangesAsync();

            return userToCreate;
        }
    }
}
