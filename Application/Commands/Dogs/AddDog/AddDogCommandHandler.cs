using Domain.Models;
using Infrastructure.DataDbContex;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


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
            Dog dogToCreate = new()
            {
                AnimalId = Guid.NewGuid(),
                Name = request.NewDog.Name,
                BreedDog = request.NewDog.BreedDog,
                WeightDog = request.NewDog.WeightDog,

            };

            await _dataDbContex.Dogs.AddAsync(dogToCreate);
            await _dataDbContex.SaveChangesAsync();

            return dogToCreate;
        }

    }
}