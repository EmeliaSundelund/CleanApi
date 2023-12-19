using Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Cats.CatByBreedOrWeight
{
    public class CatByBreedQuery : IRequest<List<Cat>>
    {
        public CatByBreedQuery(string breedCat, int? weightCat)
        {
            BreedCat = breedCat;
            WeightCat = weightCat;
        }

        public string BreedCat { get; }
        public int? WeightCat { get; }
    }
}
