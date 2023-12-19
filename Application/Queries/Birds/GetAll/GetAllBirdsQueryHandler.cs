using Application.Queries.Birds.GetAll;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.DataDbContex;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;


namespace Application.Queries.Birds
{
    public class GetAllBirdsQueryHandler : IRequestHandler<GetAllBirdsQuery, List<Bird>>
    {

        private readonly IAnimalsRepository _animalRepository;

        public GetAllBirdsQueryHandler(IAnimalsRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<List<Bird>> Handle(GetAllBirdsQuery request, CancellationToken cancellationToken)
        {
            List<Bird> allBirds = await _animalRepository.GetAllBirdsAsync();
            return allBirds;
        }
    }
}
