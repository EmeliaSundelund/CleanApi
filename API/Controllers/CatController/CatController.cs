using Application.Commands.Cats.AddCat;
using Application.Commands.Cats.DeleteCat;
using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Application.Queries.Cats.CatByBreedOrWeight;
using Application.Queries.Cats.GetAll;
using Application.Queries.Cats.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CatsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CatsController> _logger; 

        public CatsController(IMediator mediator, ILogger<CatsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        [Route("getAllCats")]
        public async Task<IActionResult> GetAllCats()
        {
            try
            {
                _logger.LogInformation("Executing GetAllCats method.");

                var result = await _mediator.Send(new GetAllCatsQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in GetAllCats method.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Authorize]
        [Route("getCatById/{catId}")]
        public async Task<IActionResult> GetCatById(Guid catId)
        {
            try
            {
                _logger.LogInformation($"Executing GetCatById method for cat ID: {catId}");

                var result = await _mediator.Send(new GetCatByIdQuery(catId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in GetCatById method for cat ID: {catId}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getCatByBreed")]
        [Authorize]
        public async Task<IActionResult> GetCatByBreed(string breedCat = null, int? weightCat = null)
        {
            try
            {
                _logger.LogInformation($"Executing GetCatByBreed method for breed: {breedCat}, weight: {weightCat}");

                var result = await _mediator.Send(new CatByBreedQuery(breedCat, weightCat));
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in GetCatByBreed method for breed: {breedCat}, weight: {weightCat}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Authorize]
        [Route("addNewCat")]
        public async Task<IActionResult> AddCat([FromBody] CatDto newCat)
        {
            try
            {
                _logger.LogInformation("Executing AddCat method.");

                return Ok(await _mediator.Send(new AddCatCommand(newCat)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in AddCat method.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Authorize]
        [Route("updateCat/{updatedCatId}")]
        public async Task<IActionResult> UpdateCat([FromBody] CatDto updatedCat, Guid updatedCatId)
        {
            try
            {
                _logger.LogInformation($"Executing UpdateCat method for cat ID: {updatedCatId}");

                return Ok(await _mediator.Send(new UpdateCatByIdCommand(updatedCat, updatedCatId)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in UpdateCat method for cat ID: {updatedCatId}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("deleteCat/{deletedCatId}")]
        public async Task<IActionResult> DeleteCat(Guid deletedCatId)
        {
            try
            {
                _logger.LogInformation($"Executing DeleteCat method for cat ID: {deletedCatId}");

                return Ok(await _mediator.Send(new DeleteCatByIdCommand(deletedCatId)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in DeleteCat method for cat ID: {deletedCatId}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}