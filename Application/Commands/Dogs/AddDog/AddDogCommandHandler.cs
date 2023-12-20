using Domain.Models;
using Infrastructure.DataDbContex;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; 
namespace Application.Commands.Dogs
{
    public class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
    {
        private readonly IConfiguration _configuration;
        private readonly DataDbContex _dataDbContex;

        public AddDogCommandHandler(IConfiguration configuration, DataDbContex dataDbContex)
        {
            _configuration = configuration;
            _dataDbContex = dataDbContex;
        }

        public async Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine("Handling AddDogCommand.");

                Dog dogToCreate = new()
                {
                    AnimalId = Guid.NewGuid(),
                    Name = request.NewDog.Name,
                    BreedDog = request.NewDog.BreedDog,
                    WeightDog = request.NewDog.WeightDog,
                };

                await _dataDbContex.Dogs.AddAsync(dogToCreate);
                await _dataDbContex.SaveChangesAsync();

                Console.WriteLine("Added new dog with ID: {dogToCreate.AnimalId}");

                return dogToCreate;
            }
            catch (Exception)
            {
                Console.WriteLine("Error handling AddDogCommand: {ex.Message}");
                throw;
            }
        }
    }
}