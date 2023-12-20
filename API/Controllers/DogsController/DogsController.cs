using Application.Commands.Dogs;
using Application.Commands.Dogs.DeleteDog;
using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Application.Queries.Birds.GetAllColor;
using Application.Queries.Dogs.DogByBreedOrWeight;
using Application.Queries.Dogs.GetAll;
using Application.Queries.Dogs.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DogsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        internal readonly IMediator _mediator;
        public DogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

  
        [HttpGet]
        [Authorize]
        [Route("getAllDogs")]
        public async Task<IActionResult> GetAllDogs()
        {
            return Ok(await _mediator.Send(new GetAllDogsQuery()));
            
        }

      
        [HttpGet]
        [Authorize]
        [Route("getDogById/{dogId}")]
        public async Task<IActionResult> GetDogById(Guid dogId)
        {
            return Ok(await _mediator.Send(new GetDogByIdQuery(dogId)));
        }

        [HttpGet("getDogByBreed")]
        [Authorize]
        public async Task<IActionResult> GetDogByBreed(string breedDog = null, int? weightDog = null)
        {
            var result = await _mediator.Send(new DogByBreedQuery(breedDog, weightDog));
            return Ok(result);
        }

        [HttpPost]
        [Route("addNewDog")]
        [Authorize]
        public async Task<IActionResult> AddDog([FromBody] DogDto newDog)
        {
            return Ok(await _mediator.Send(new AddDogCommand(newDog)));
        }

        [HttpPut]
        [Authorize]
        [Route("updateDog/{updatedDogId}")]
        public async Task<IActionResult> UpdateDog([FromBody] DogDto updatedDog, Guid updatedDogId)
        {
            return Ok(await _mediator.Send(new UpdateDogByIdCommand(updatedDog, updatedDogId)));
        }

        [HttpDelete]
        [Authorize]
        [Route("deleteDog/{deletedDogId}")]
        public async Task<IActionResult> DeleteDog(Guid deletedDogId)
        {
            return Ok(await _mediator.Send(new DeleteDogByIdCommand(deletedDogId)));
        }

    }
}
