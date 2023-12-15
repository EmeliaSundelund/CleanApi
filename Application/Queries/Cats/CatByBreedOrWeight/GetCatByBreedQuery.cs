using Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Cats.CatByBreedOrWeight
{
    public class GetCatByBreedQuery : IRequest<List<Cat>>
    {
        public GetCatByBreedQuery(string breedCat, int? weightCat)
        {
            BreedCat = breedCat;
            WeightCat = weightCat;
        }

        public string BreedCat { get; }
        public int? WeightCat { get; }
    }
}
