using Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Cats.AddCat
{
    public class AddCatCommandHandler : IRequestHandler<AddCatCommand, Cat>
    {
        private readonly IConfiguration _configuration;
        private readonly Infrastructure.DataDbContex.DataDbContex _dataDbContex;
        private readonly ILogger<AddCatCommandHandler> _logger;

        public AddCatCommandHandler(IConfiguration configuration, Infrastructure.DataDbContex.DataDbContex dataDbContex, ILogger<AddCatCommandHandler> logger)
        {
            _configuration = configuration;
            _dataDbContex = dataDbContex;
            _logger = logger;
        }

        public async Task<Cat> Handle(AddCatCommand request, CancellationToken cancellationToken)
        {
            try
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

                _logger.LogInformation($"Added new cat with ID: {catToCreate.AnimalId}");

                return catToCreate;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error handling AddCatCommand: {ex.Message}");
                throw;
            }
        }
    }
}