using Application.Commands.User.AddUser;
using Application.Commands.User.UpdateUser;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Commands.Users.DeleteUser;
using Application.Commands.Dogs.UpdateDog;
using Application.Queries.Dogs.GetAll;
using Application.Queries.Users.GetAll;
using Application.Queries.Dogs.GetById;
using Application.Queries.Users.GetById;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.DogsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        internal readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all dogs from database
        [HttpGet]
        [Route("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _mediator.Send(new GetAllUsersQuery()));
            //return Ok("GET ALL DOGS");
        }

        // Create a new dog 
        [HttpPost]
        [Route("addNewUser")]
        public async Task<IActionResult> AddUser([FromBody] UserDto newUser)
        {
            return Ok(await _mediator.Send(new AddUserCommand(newUser)));
        }

        // Get a dog by Id
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

        // Update a specific dog
        [HttpPut]
        [Route("updateUser/{updatedUserId}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto updatedUser, Guid updatedUserId)
        {
            return Ok(await _mediator.Send(new UpdateUserByIdCommand(updatedUser, updatedUserId)));
        }
    }
}
