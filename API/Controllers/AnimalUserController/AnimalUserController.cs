using Application.Commands.AnimalUser.AddAnimalUser;
using Application.Commands.AnimalUser.DeleteAnimalUser;
using Application.Commands.AnimalUser.UpdateAnimalUser;
using Application.Dtos;
using Application.Queries.AnimalUser.GetAllAnimalUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AnimalUserController
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
        //en kommentar 
        [HttpGet]
        [Route("getAllAnimalUsers")]
        public async Task<IActionResult> GetAllAnimalUsers()
        {
            try
            {
                var animalUsers = await _mediator.Send(new GetAllAnimalUsersQuery());
                return animalUsers == null ? NotFound("No animalUsers found.") : Ok(animalUsers);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Servor Error");
            }
        }

        [HttpPost]
        [Route("addNewAnimalUser")]
        public async Task<IActionResult> AddAnimalUser([FromBody] AnimalUserDto newAnimalUser)
        {
            try
            {
                var result = await _mediator.Send(new AddAnimalUserCommand(newAnimalUser));
                return result == false ? BadRequest("Could not add the animaluser.") : Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Servor Error");
            }
        }

        [HttpPost]
        [Route("updateAnimalUser")]
        public async Task<IActionResult> UpdateAnimalUser([FromBody] UpdateAnimalUserByUserIdCommand command)
        {

            var result = await _mediator.Send(command);
            return command == null ? BadRequest("Invalid update animal user command data.") : Ok(result);

        }

        [HttpDelete]
        [Route("deleteAnimalUser/{deletedAnimalUserKey}")]
        public async Task<IActionResult> DeleteAnimalUser(Guid deletedAnimalUser)
        {

            var result = await _mediator.Send(new DeleteAnimalUserCommand(deletedAnimalUser));
            return result == false ? BadRequest("Invalid delete animal user command data.") : Ok(result);

        }
    }
}