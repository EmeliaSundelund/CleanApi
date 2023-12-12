using Domain.Models;
using Infrastructure.DataDbContex;
using MediatR;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Application.Commands.Birds.UpdateBird
{
    public class UpdateBirdByIdCommandHandler : IRequestHandler<UpdateBirdByIdCommand, Bird>
    {
        private readonly IAnimalsRepository _animalRepository;

        public UpdateBirdByIdCommandHandler(IAnimalsRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<Bird> Handle(UpdateBirdByIdCommand request, CancellationToken cancellationToken)
        {
            var birdToUpdate = await _animalRepository.GetByIdAsync(request.Id) as Bird;

            if (birdToUpdate != null)
            {
                birdToUpdate.Name = request.UpdatedBird.Name;
                birdToUpdate.Color = request.UpdatedBird.Color;

                await _animalRepository.UpdateAsync(birdToUpdate);

                return birdToUpdate;
            }
            else
            {
                throw new InvalidOperationException($"Bird with ID {request.Id} not found.");
            }
        }
    }
}
