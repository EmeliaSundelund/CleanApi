using Application.Commands.Cats.AddCat;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.DataDbContex;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Commands.Cats.AddCat
{
    namespace Application.Commands.Cats
    {
        public class AddCatCommandHandler : IRequestHandler<AddCatCommand, Cat>
        {
            private readonly IConfiguration _configuration;

            private readonly Infrastructure.DataDbContex.DataDbContex _dataDbContex;

            public AddCatCommandHandler(IConfiguration configuration, Infrastructure.DataDbContex.DataDbContex dataDbContex)
            {
                _configuration = configuration;
                _dataDbContex = dataDbContex;
            }

            public async Task<Cat> Handle(AddCatCommand request, CancellationToken cancellationToken)
            {
                Cat catToCreate = new()
                {
                    AnimalId = Guid.NewGuid(),
                    Name = request.NewCat.Name,
                    BreedCat = request.NewCat.BreedCat,
                    WeightCat = request.NewCat.WeightCat,

                };

                await _dataDbContex.Cats.AddAsync(catToCreate);
                await _dataDbContex.SaveChangesAsync();

                return catToCreate;
            }
        }
    }

}