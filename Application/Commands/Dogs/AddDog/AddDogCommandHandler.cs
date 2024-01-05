using Domain.Models;
using Infrastructure.DataDbContex;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Dogs
{
    public class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
    {
        private readonly IConfiguration _configuration;
        private readonly DataDbContex _dataDbContex;
        private readonly ILogger<AddDogCommandHandler> _logger;

        public AddDogCommandHandler(IConfiguration configuration, DataDbContex dataDbContex, ILogger<AddDogCommandHandler> logger)
        {
            _configuration = configuration;
            _dataDbContex = dataDbContex;
            _logger = logger;
        }

        public async Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling AddDogCommand.");

                Dog dogToCreate = new()
                {
                    AnimalId = Guid.NewGuid(),
                    Name = request.NewDog.Name,
                    BreedDog = request.NewDog.BreedDog,
                    WeightDog = request.NewDog.WeightDog,
                };

                await _dataDbContex.Dogs.AddAsync(dogToCreate);
                await _dataDbContex.SaveChangesAsync();

                _logger.LogInformation($"Added new dog with ID: {dogToCreate.AnimalId}");

                return dogToCreate;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error handling AddDogCommand: {ex.Message}");
                throw;
            }
        }
    }
}