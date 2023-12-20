using Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Dogs.DogByBreedOrWeight
{
    public class DogByBreedQuery : IRequest<List<Dog>>
    {
        public DogByBreedQuery(string breedDog, int? weightDog)
        {
            BreedDog = breedDog;
            WeightDog = weightDog;
        }

        public string BreedDog { get; }
        public int? WeightDog { get; }
    }
}