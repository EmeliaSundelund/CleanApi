using Domain.Models.Person;
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
            try
            {
                Console.WriteLine("Handling AddUserCommand");

                UserModel userToCreate = new()
                {
                    UserId = Guid.NewGuid(),
                    UserName = request.NewUser.UserName,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.NewUser.Password),
                };

                await _dataDbContex.Person.AddAsync(userToCreate);
                await _dataDbContex.SaveChangesAsync();

                Console.WriteLine("Added new user with ID");

                return userToCreate;
            }
            catch (Exception)
            {
                Console.WriteLine("Error handling AddUserCommand");
                throw;
            }
        }
    }
}
