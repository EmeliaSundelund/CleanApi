using Application.Commands.AnimalUser.AddAnimalUser;
using Application.Commands.AnimalUser.DeleteAnimalUser;
using Application.Commands.AnimalUser.UpdateAnimalUser;
using Application.Dtos;
using Application.Queries.AnimalUser.GetAllAnimalUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [Route("getAllAnimalUsers")]
        public async Task<IActionResult> GetAllAnimalUsers()
        {
            try
            {
                var animalUsers = await _mediator.Send(new GetAllAnimalUsersQuery());

                if (animalUsers == null)
                {
                    ModelState.AddModelError("NotFound", "No animalUsers found.");
                    return BadRequest(ModelState);
                }

                return Ok(animalUsers);
            }
            catch (Exception)
            {
                ModelState.AddModelError("ServerError", "Internal Server Error");
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost]
        [Authorize]
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
        [Authorize]
        [Route("updateAnimalUser")]
        public async Task<IActionResult> UpdateAnimalUser([FromBody] UpdateAnimalUserByUserIdCommand command)
        {

            var result = await _mediator.Send(command);
            return command == null ? BadRequest("Invalid update animal user command data.") : Ok(result);

        }

        [HttpDelete]
        [Authorize]
        [Route("deleteAnimalUser")]
        public async Task<IActionResult> DeleteAnimalUser(Guid deletedAnimalUser)
        {

            var result = await _mediator.Send(new DeleteAnimalUserCommand(deletedAnimalUser));
            return result == false ? BadRequest("Invalid delete animal user command data.") : Ok(result);

        }
    }
}