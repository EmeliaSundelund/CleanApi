using Application.Commands.Birds.AddBird;
using Application.Commands.Birds.DeleteBird;
using Application.Commands.Birds.UpdateBird;
using Application.Dtos;
using Application.Queries.Birds.GetAll;
using Application.Queries.Birds.GetAllColor;
using Application.Queries.Birds.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 

namespace API.Controllers.CatsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BirdsController> _logger; 

        public BirdsController(IMediator mediator, ILogger<BirdsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        [Route("getAllBirds")]
        public async Task<IActionResult> GetAllBirds()
        {
            try
            {
                _logger.LogInformation("Executing GetAllBirds method.");

                var result = await _mediator.Send(new GetAllBirdsQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in GetAllBirds method.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Authorize]
        [Route("getBirdById/{birdId}")]
        public async Task<IActionResult> GetBirdById(Guid birdId)
        {
            try
            {
                _logger.LogInformation($"Executing GetBirdById method for bird ID: {birdId}");

                var result = await _mediator.Send(new GetBirdByIdQuery(birdId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in GetBirdById method for bird ID: {birdId}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Authorize]
        [Route("getBirdByColor/{birdColor}")]
        public async Task<IActionResult> GetBirdByColor(string birdColor)
        {
            try
            {
                _logger.LogInformation($"Executing GetBirdByColor method for color: {birdColor}");

                var result = await _mediator.Send(new GetBirdsByColorQuery(birdColor));
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in GetBirdByColor method for color: {birdColor}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("addNewBird")]
        public async Task<IActionResult> AddBird([FromBody] BirdDto newBird)
        {
            try
            {
                _logger.LogInformation("Executing AddBird method.");

                // Utför modellvalidering baserat på Data Annotations
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Om modellvalidering lyckas, skicka kommandot för att lägga till fågeln
                var result = await _mediator.Send(new AddBirdCommand(newBird));
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in AddBird method.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Authorize]
        [Route("updateBird/{updatedBirdId}")]
        public async Task<IActionResult> UpdateBird([FromBody] BirdDto updatedBird, Guid updatedBirdId)
        {
            try
            {
                _logger.LogInformation($"Executing UpdateBird method for bird ID: {updatedBirdId}");

                // Utför modellvalidering baserat på Data Annotations
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Om modellvalidering lyckas, skicka kommandot för att uppdatera fågeln
                var result = await _mediator.Send(new UpdateBirdByIdCommand(updatedBird, updatedBirdId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in UpdateBird method for bird ID: {updatedBirdId}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("deleteBird/{deletedBirdId}")]
        public async Task<IActionResult> DeleteBird(Guid deletedBirdId)
        {
            try
            {
                _logger.LogInformation($"Executing DeleteBird method for bird ID: {deletedBirdId}");

                var result = await _mediator.Send(new DeleteBirdByIdCommand(deletedBirdId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in DeleteBird method for bird ID: {deletedBirdId}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
