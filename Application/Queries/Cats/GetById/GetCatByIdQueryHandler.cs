using Application.Queries.Birds.GetById;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.DataDbContex;
using MediatR;

namespace Application.Queries.Cats.GetById
{
    public class GetCatByIdQueryHandler : IRequestHandler<GetCatByIdQuery, Cat>
    {
        private readonly IAnimalsRepository _animalRepository;

        public GetCatByIdQueryHandler(IAnimalsRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<Cat> Handle(GetCatByIdQuery request, CancellationToken cancellationToken)
        {
            Cat wantedCat = await _animalRepository.GetByIdAsync(request.Id) as Cat;

            return wantedCat;
        }

    }
}
