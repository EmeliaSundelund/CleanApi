using Domain.Models.Person;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Commands.User.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserModel>
    {
        private readonly IConfiguration _configuration;
        private readonly Infrastructure.DataDbContex.DataDbContex _dataDbContex;
        private readonly ILogger<AddUserCommandHandler> _logger;

        public AddUserCommandHandler(IConfiguration configuration, Infrastructure.DataDbContex.DataDbContex dataDbContex, ILogger<AddUserCommandHandler> logger)
        {
            _configuration = configuration;
            _dataDbContex = dataDbContex;
            _logger = logger;
        }

        public async Task<UserModel> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling AddUserCommand");

                UserModel userToCreate = new()
                {
                    UserId = Guid.NewGuid(),
                    UserName = request.NewUser.UserName,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.NewUser.Password),
                };

                await _dataDbContex.Person.AddAsync(userToCreate);
                await _dataDbContex.SaveChangesAsync();

                _logger.LogInformation($"Added new user with ID: {userToCreate.UserId}");

                return userToCreate;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error handling AddUserCommand: {ex.Message}");
                throw;
            }
        }
    }
}