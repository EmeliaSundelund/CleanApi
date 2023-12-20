using Application.Commands.Birds.AddBird;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Commands.Birds
{
    public class AddBirdCommandHandler : IRequestHandler<AddBirdCommand, Bird>
    {
        private readonly IConfiguration _configuration;
        private readonly Infrastructure.DataDbContex.DataDbContex _dataDbContex;

        public AddBirdCommandHandler(IConfiguration configuration, Infrastructure.DataDbContex.DataDbContex dataDbContex)
        {
            _configuration = configuration;
            _dataDbContex = dataDbContex;

        }

        public async Task<Bird> Handle(AddBirdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Bird birdToCreate = new()
                {
                    AnimalId = Guid.NewGuid(),
                    Name = request.NewBird.Name,
                    Color = request.NewBird.Color,
                    CanFly = request.NewBird.CanFly,
                };

                await _dataDbContex.Birds.AddAsync(birdToCreate);
                await _dataDbContex.SaveChangesAsync();

                Console.WriteLine($"Added new bird with ID: {birdToCreate.AnimalId}");

                return birdToCreate;
            }
            catch (Exception)
            {
                Console.WriteLine("Error handling AddBirdCommand: {ex.Message}");
                // You might want to handle the exception appropriately or propagate it
                throw;
            }
        }
    }
}