using Application.Commands.Bird.AddBird;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Bird
{
    public class AddBirdCommandHandler : IRequestHandler<AddBirdCommand, Domain.Models.Bird>
    {
        private readonly IConfiguration _configuration;
        private readonly Infrastructure.DataDbContex.DataDbContex _dataDbContex;
        private readonly ILogger<AddBirdCommandHandler> _logger;

        public AddBirdCommandHandler(IConfiguration configuration, Infrastructure.DataDbContex.DataDbContex dataDbContex, ILogger<AddBirdCommandHandler> logger)
        {
            _configuration = configuration;
            _dataDbContex = dataDbContex;
            _logger = logger;
        }

        public async Task<Domain.Models.Bird> Handle(AddBirdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Domain.Models.Bird birdToCreate = new()
                {
                    AnimalId = Guid.NewGuid(),
                    Name = request.NewBird.Name,
                    Color = request.NewBird.Color,
                    CanFly = request.NewBird.CanFly,
                };

                await _dataDbContex.Birds.AddAsync(birdToCreate);
                await _dataDbContex.SaveChangesAsync();

                _logger.LogInformation($"Added new bird with ID: {birdToCreate.AnimalId}");

                return birdToCreate;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error handling AddBirdCommand: {ex.Message}");
                throw;
            }
        }
    }
}