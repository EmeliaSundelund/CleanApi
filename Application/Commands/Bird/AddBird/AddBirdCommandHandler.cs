using Application.Commands.Birds.AddBird;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.DataDbContex;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Commands.Birds
{
    public class AddBirdCommandHandler : IRequestHandler<AddBirdCommand, Bird>
    {
        private readonly IConfiguration _configuration;

        private readonly DataDbContex _dataDbContex;

        public AddBirdCommandHandler(IConfiguration configuration, DataDbContex dataDbContex)
        {
            _configuration = configuration;
            _dataDbContex = dataDbContex;
        }

        public async Task<Bird> Handle(AddBirdCommand request, CancellationToken cancellationToken)
        {
            Bird birdToCreate = new()
            {
                id = Guid.NewGuid(),
                Name = request.NewBird.Name,
                Color = request.NewBird.Color,

            };

            await _dataDbContex.Birds.AddAsync(birdToCreate);
            await _dataDbContex.SaveChangesAsync();

            return birdToCreate;
        }
    }
}
