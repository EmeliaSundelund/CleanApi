using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Domain.Models.Person;
using MediatR;
using Infrastructure.DataDbContex;

namespace Application.Commands.User.LogInUser
{
    public class LogInCommandHandler : IRequestHandler<LogInCommand, string>
    {
        private readonly IConfiguration _configuration;
        private readonly DataDbContex _dataDbContext;

        public LogInCommandHandler(IConfiguration configuration, DataDbContex dataDbContext)
        {
            _configuration = configuration;
            _dataDbContext = dataDbContext;
        }

        public async Task<string> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            var user = _dataDbContext.Person.SingleOrDefault(u => u.UserName == request.LogInDto.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.LogInDto.Password, user.Password))
            {
                throw new Exception("Invalid username or password.");
            }

            // Generera och returnera JWT-token
            return GenerateToken(user);
        }

        private string GenerateToken(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "User"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:TokenKey").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}