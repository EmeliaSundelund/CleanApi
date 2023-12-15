using Domain.Models;
using Infrastructure.DataDbContex;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Dogs.DogByBreedOrWeight
{
    public class GetDogsByBreedQueryHandler : IRequestHandler<GetDogByBreedQuery, List<Dog>>
    {
        private readonly IAnimalsRepository _animalRepository;

        public GetDogsByBreedQueryHandler(IAnimalsRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<List<Dog>> Handle(GetDogByBreedQuery request, CancellationToken cancellationToken)
        {
            List<Dog> dogs;

            if (request.WeightDog.HasValue)
            {
                dogs = await _animalRepository.GetDogsByWeightAsync(request.WeightDog.Value);
            }
            else
            {
                dogs = await _animalRepository.GetDogsByBreedAsync(request.BreedDog);
            }

            dogs = dogs.OrderByDescending(d => d.Name).ToList();

            return dogs;
        }

    }
}
