using Application.Queries.Cats.GetAll;
using Domain.Models;
using Infrastructure.DataDbContex;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Cats
{
    public class GetAllCatsQueryHandler : IRequestHandler<GetAllCatsQuery, List<Cat>>
    {
        private readonly IAnimalsRepository _animalRepository;

        public GetAllCatsQueryHandler(IAnimalsRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<List<Cat>> Handle(GetAllCatsQuery request, CancellationToken cancellationToken)
        {
            List<Cat> allCats = await _animalRepository.GetAllCatsAsync();
            return allCats;
        }
    }
}
