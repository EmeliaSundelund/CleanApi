using Application.Commands.AnimalUser.AddAnimalUser;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalUserController : ControllerBase
    {
        internal readonly IMediator _mediator;
        public AnimalUserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /*
        // Get all animal users from the database
        [HttpGet]
        [Route("getAllAnimalUsers")]
        public async Task<IActionResult> GetAllAnimalUsers()
        {
            return Ok(await _mediator.Send(new GetAllAnimalUsersQuery()));
        }
        */
        // Create a new animal user 
        [HttpPost]
        [Route("AddnewAnimalUser")]
        public async Task<IActionResult> AddUserAnimalAsync([FromBody] AnimalUserDto newAnimalUser)
        {
            return Ok(await _mediator.Send(new AddAnimalUserCommand(newAnimalUser)));
        }
        /*
        // Get an animal user by Id
        [HttpGet]
        [Route("getAnimalUserById/{animalUserId}")]
        public async Task<IActionResult> GetAnimalUserById(Guid animalUserId)
        {
            return Ok(await _mediator.Send(new GetAnimalUserByIdQuery(animalUserId)));
        }

        [HttpDelete]
        [Route("deleteAnimalUser/{deletedAnimalUserId}")]
        public async Task<IActionResult> DeleteAnimalUser(Guid deletedAnimalUserId)
        {
            return Ok(await _mediator.Send(new DeleteAnimalUserByIdCommand(deletedAnimalUserId)));
        }

        // Update a specific animal user
        [HttpPut]
        [Route("updateAnimalUser/{updatedAnimalUserId}")]
        public async Task<IActionResult> UpdateAnimalUser([FromBody] UserDto updatedAnimalUser, Guid updatedAnimalUserId)
        {
            return Ok(await _mediator.Send(new UpdateAnimalUserByIdCommand(updatedAnimalUser, updatedAnimalUserId)));
        }
        */
    }
    
}

