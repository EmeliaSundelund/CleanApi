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
using Microsoft.AspNetCore.Authorization;
using Application.Commands.User.LogInUser;
using System;

namespace API.Controllers.UsersController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITokenService _tokenService;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ITokenService tokenService, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        [Route("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                _logger.LogInformation("Executing GetAllUsers method.");

                return Ok(await _mediator.Send(new GetAllUsersQuery()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in GetAllUsers method.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("addNewUser")]
        public async Task<IActionResult> AddUser([FromBody] UserDto newUser)
        {
            try
            {
                _logger.LogInformation("Executing AddUser method.");

                return Ok(await _mediator.Send(new AddUserCommand(newUser)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in AddUser method.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("logIn")]
        public async Task<IActionResult> LogIn([FromBody] UserDto logInDto)
        {
            try
            {
                _logger.LogInformation("Executing LogIn method.");

                var token = await _mediator.Send(new LogInCommand(logInDto));

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in LogIn method.");
                return BadRequest($"Error logging in: {ex.Message}");
            }
        }

        [HttpGet]
        [Authorize]
        [Route("getUserById/{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            try
            {
                _logger.LogInformation($"Executing GetUserById method for user ID: {userId}");

                return Ok(await _mediator.Send(new GetUserByIdQuery(userId)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in GetUserById method for user ID: {userId}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("deleteUser/{deletedUserId}")]
        public async Task<IActionResult> DeleteUser(Guid deletedUserId)
        {
            try
            {
                _logger.LogInformation($"Executing DeleteUser method for user ID: {deletedUserId}");

                return Ok(await _mediator.Send(new DeleteUserByIdCommand(deletedUserId)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in DeleteUser method for user ID: {deletedUserId}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Authorize]
        [Route("updateUser/{updatedUserId}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto updatedUser, Guid updatedUserId)
        {
            try
            {
                _logger.LogInformation($"Executing UpdateUser method for user ID: {updatedUserId}");

                return Ok(await _mediator.Send(new UpdateUserByIdCommand(updatedUser, updatedUserId)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in UpdateUser method for user ID: {updatedUserId}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}