using Domain.Models;
using Infrastructure.DataDbContex;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;

namespace Application.Queries.Birds.GetById
{
    public class GetBirdByIdQueryHandler : IRequestHandler<GetBirdByIdQuery, Bird>
    {
        private readonly IAnimalsRepository _animalRepository;

        public GetBirdByIdQueryHandler(IAnimalsRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<Bird> Handle(GetBirdByIdQuery request, CancellationToken cancellationToken)
        {
            Bird wantedBird = await _animalRepository.GetByIdAsync(request.Id) as Bird;

            return wantedBird;
        }
    }
}
