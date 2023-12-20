using Domain.Models;
using Infrastructure.DataDbContex;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;

namespace Application.Queries.Cats.CatByBreedOrWeight
{
    public class CatsByBreedQueryHandler : IRequestHandler<CatByBreedQuery, List<Cat>>
    {
        private readonly IAnimalsRepository _animalRepository;

        public CatsByBreedQueryHandler(IAnimalsRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<List<Cat>> Handle(CatByBreedQuery request, CancellationToken cancellationToken)
        {
            List<Cat> cats;

            if (request.WeightCat.HasValue)
            {
                cats = await _animalRepository.GetCatsByWeightAsync(request.WeightCat.Value);
            }
            else
            {
                cats = await _animalRepository.GetCatsByBreedAsync(request.BreedCat);
            }

            cats = cats.OrderByDescending(d => d.Name).ToList();

            return cats;
        }

    }
}
