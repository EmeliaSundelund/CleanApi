using Domain.Models;
using Infrastructure.DataDbContex;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Dogs.DogByBreedOrWeight
{
    public class DogsByBreedQueryHandler : IRequestHandler<DogByBreedQuery, List<Dog>>
    {
        private readonly IAnimalsRepository _animalRepository;

        public DogsByBreedQueryHandler(IAnimalsRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<List<Dog>> Handle(DogByBreedQuery request, CancellationToken cancellationToken)
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
