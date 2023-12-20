using Application.Commands.Dogs;
using Application.Commands.Dogs.DeleteDog;
using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
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
        private readonly ILogger<DogsController> _logger; 

        public DogsController(IMediator mediator, ILogger<DogsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        [Route("getAllDogs")]
        public async Task<IActionResult> GetAllDogs()
        {
            try
            {
                _logger.LogInformation("Executing GetAllDogs method.");

                return Ok(await _mediator.Send(new GetAllDogsQuery()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in GetAllDogs method.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Authorize]
        [Route("getDogById/{dogId}")]
        public async Task<IActionResult> GetDogById(Guid dogId)
        {
            try
            {
                _logger.LogInformation($"Executing GetDogById method for dog ID: {dogId}");

                return Ok(await _mediator.Send(new GetDogByIdQuery(dogId)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in GetDogById method for dog ID: {dogId}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getDogByBreed")]
        [Authorize]
        public async Task<IActionResult> GetDogByBreed(string breedDog = null, int? weightDog = null)
        {
            try
            {
                _logger.LogInformation($"Executing GetDogByBreed method for breed: {breedDog}, weight: {weightDog}");

                var result = await _mediator.Send(new DogByBreedQuery(breedDog, weightDog));
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in GetDogByBreed method for breed: {breedDog}, weight: {weightDog}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("addNewDog")]
        [Authorize]
        public async Task<IActionResult> AddDog([FromBody] DogDto newDog)
        {
            try
            {
                _logger.LogInformation("Executing AddDog method.");

                return Ok(await _mediator.Send(new AddDogCommand(newDog)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in AddDog method.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Authorize]
        [Route("updateDog/{updatedDogId}")]
        public async Task<IActionResult> UpdateDog([FromBody] DogDto updatedDog, Guid updatedDogId)
        {
            try
            {
                _logger.LogInformation($"Executing UpdateDog method for dog ID: {updatedDogId}");

                return Ok(await _mediator.Send(new UpdateDogByIdCommand(updatedDog, updatedDogId)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in UpdateDog method for dog ID: {updatedDogId}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("deleteDog/{deletedDogId}")]
        public async Task<IActionResult> DeleteDog(Guid deletedDogId)
        {
            try
            {
                _logger.LogInformation($"Executing DeleteDog method for dog ID: {deletedDogId}");

                return Ok(await _mediator.Send(new DeleteDogByIdCommand(deletedDogId)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in DeleteDog method for dog ID: {deletedDogId}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}