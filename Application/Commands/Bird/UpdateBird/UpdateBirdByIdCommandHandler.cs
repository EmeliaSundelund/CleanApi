using Domain.Models;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;

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
            try
            {
                Console.WriteLine("Handling UpdateBirdByIdCommand for Bird ID: {request.AnimalId}");

                var birdToUpdate = await _animalRepository.GetByIdAsync(request.AnimalId) as Bird;

                if (birdToUpdate != null)
                {
                    birdToUpdate.Name = request.UpdatedBird.Name;
                    birdToUpdate.Color = request.UpdatedBird.Color;

                    await _animalRepository.UpdateAsync(birdToUpdate);

                    Console.WriteLine("Updated Bird with ID: {request.AnimalId}");

                    return birdToUpdate;
                }
                else
                {
                    throw new InvalidOperationException($"Bird with ID {request.AnimalId} not found.");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error handling UpdateBirdByIdCommand: {ex.Message}");
                throw;
            }
        }
    }
}