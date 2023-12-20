using Application.Queries.Birds.GetById;
using Domain.Models;
using Infrastructure.DataDbContex;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Birds.GetAllColor
{
    public class GetBirdsByColorQueryHandler : IRequestHandler<GetBirdsByColorQuery, List<Bird>>
    {
        private readonly IAnimalsRepository _animalRepository;

        public GetBirdsByColorQueryHandler(IAnimalsRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<List<Bird>> Handle(GetBirdsByColorQuery request, CancellationToken cancellationToken)
        {
            List<Bird> colorBirds = await _animalRepository.GetBirdsByColorAsync(request.Color);

            colorBirds = colorBirds.OrderByDescending(b => b.Name).ToList();

            return colorBirds;
        }
    }
}