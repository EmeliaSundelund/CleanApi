using Application.Commands.Cats.AddCat;
using Application.Commands.Cats.DeleteCat;
using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Application.Queries.Cats.CatByBreedOrWeight;
using Application.Queries.Cats.GetAll;
using Application.Queries.Cats.GetById;
using Application.Queries.Dogs.DogByBreedOrWeight;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CatsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        internal readonly IMediator _mediator;
        public CatsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getAllCats")]
        public async Task<IActionResult> GetAllCats()
        {
            return Ok(await _mediator.Send(new GetAllCatsQuery()));
        }

        [HttpGet]
        [Route("getCatById/{catId}")]
        public async Task<IActionResult> GetCatById(Guid catId)
        {
            return Ok(await _mediator.Send(new GetCatByIdQuery(catId)));
        }

        [HttpGet("getCatByBreed")]
        public async Task<IActionResult> GetCatByBreed(string breedCat = null, int? weightCat = null)
        {
            var result = await _mediator.Send(new CatByBreedQuery(breedCat, weightCat));
            return Ok(result);
        }

        [HttpPost]
        [Route("addNewCat")]
        public async Task<IActionResult> AddCat([FromBody] CatDto newCat)
        {
            return Ok(await _mediator.Send(new AddCatCommand(newCat)));
        }

        // FIX here 
        [HttpPut]
        [Route("updateCat/{updatedCatId}")]
        public async Task<IActionResult> UpdateCat([FromBody] CatDto updatedCat, Guid updatedCatId)
        {
            return Ok(await _mediator.Send(new UpdateCatByIdCommand(updatedCat, updatedCatId)));
        }

        [HttpDelete]
        [Route("deleteCat/{deletedCatId}")]
        public async Task<IActionResult> DeleteCat(Guid deletedCatId)
        {
            return Ok(await _mediator.Send(new DeleteCatByIdCommand(deletedCatId)));
        }
    }
}