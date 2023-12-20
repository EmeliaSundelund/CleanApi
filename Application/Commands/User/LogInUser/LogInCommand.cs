using Application.Dtos;
using MediatR;

namespace Application.Commands.User.LogInUser
{
    public class LogInCommand : IRequest<string>
    {
        public LogInCommand(UserDto logInDto)
        {
            LogInDto = logInDto;
        }

        public UserDto LogInDto { get; }
    }
}