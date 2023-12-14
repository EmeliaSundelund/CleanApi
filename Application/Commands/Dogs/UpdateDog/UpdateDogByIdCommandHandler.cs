using Domain.Models;
using Infrastructure.DataDbContex;
using MediatR;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Application.Commands.Dogs.UpdateDog
{
    public class UpdateDogByIdCommandHandler : IRequestHandler<UpdateDogByIdCommand, Dog>
    {
        private readonly IAnimalsRepository _animalRepository;

        public UpdateDogByIdCommandHandler(IAnimalsRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }
        public async Task<Dog> Handle(UpdateDogByIdCommand request, CancellationToken cancellationToken)
        {
            var dogToUpdate = await _animalRepository.GetByIdAsync(request.Id) as Dog;

            if (dogToUpdate != null)
            {
                dogToUpdate.Name = request.UpdatedDog.Name;
                dogToUpdate.BreedDog = request.UpdatedDog.BreedDog;
                dogToUpdate.WeightDog = request.UpdatedDog.WeightDog;
                dogToUpdate.Owner = request.UpdatedDog.Owner;

                await _animalRepository.UpdateAsync(dogToUpdate);

                return dogToUpdate;
            }else
            {
                throw new InvalidOperationException($"Dog with ID {request.Id} not found.");
            }
        }
    }

}