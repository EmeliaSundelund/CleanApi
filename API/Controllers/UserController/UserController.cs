using Application.Commands.User.AddUser;
using Application.Commands.User.UpdateUser;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Commands.Users.DeleteUser;
using Application.Queries.Users.GetAll;
using Application.Queries.Users.GetById;
using Domain.Models.Person;
using API.Controllers.Token;

namespace API.Controllers.UsersController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITokenService _tokenService;

        public UserController(IMediator mediator, ITokenService tokenService)
        {
            _mediator = mediator;
            _tokenService = tokenService;
        }

        // Get all dogs from database
        [HttpGet]
        [Route("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _mediator.Send(new GetAllUsersQuery()));
            //return Ok("GET ALL DOGS");
        }

        [HttpPost]
        [Route("addNewUser")]
        public async Task<IActionResult> AddUser([FromBody] UserDto newUser)
        {
            return Ok(await _mediator.Send(new AddUserCommand(newUser)));
        }

        [HttpPost]
        [Route("logIn")]
        public async Task<IActionResult> LogIn([FromBody] UserDto newUser)
        {
            try
            {  
                var addedUser = await _mediator.Send(new AddUserCommand(newUser));

                var token = CreateToken(addedUser);

                return Ok(new { User = addedUser, Token = token });
            }
            catch (Exception ex)
            {
                
                return BadRequest($"Error adding user: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("getUserById/{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            return Ok(await _mediator.Send(new GetUserByIdQuery(userId)));
        }

        [HttpDelete]
        [Route("deleteUser/{deletedUserId}")]
        public async Task<IActionResult> DeleteUser(Guid deletedUserId)
        {
            return Ok(await _mediator.Send(new DeleteUserByIdCommand(deletedUserId)));
        }

        [HttpPut]
        [Route("updateUser/{updatedUserId}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto updatedUser, Guid updatedUserId)
        {
            return Ok(await _mediator.Send(new UpdateUserByIdCommand(updatedUser, updatedUserId)));
        }

        private string CreateToken(UserModel user)
        {
            return _tokenService.GenerateToken(user);
        }
    }
}
