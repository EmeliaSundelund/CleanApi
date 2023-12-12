using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.DataDbContex;
using MediatR;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Application.Queries.Dogs
{
    public class GetAllDogsQueryHandler : IRequestHandler<GetAllDogsQuery, List<Dog>>
    {

        private readonly IAnimalsRepository _animalRepository;

        public GetAllDogsQueryHandler(IAnimalsRepository animalRepository)

        {

            _animalRepository = animalRepository;

        }

        public async Task<List<Dog>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)

        {

            List<Dog> allDogs = await _animalRepository.GetAllDogsAsync();

            return allDogs;

        }

    }
    

}
