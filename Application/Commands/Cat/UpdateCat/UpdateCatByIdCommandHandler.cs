using Domain.Models;
using MediatR;
using Infrastructure.DataDbContex.Interfaces;
using Microsoft.Extensions.Logging; 

namespace Application.Commands.Cats.UpdateCat
{
    public class UpdateCatByIdCommandHandler : IRequestHandler<UpdateCatByIdCommand, Cat>
    {
        private readonly IAnimalsRepository _animalRepository;

        public UpdateCatByIdCommandHandler(IAnimalsRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<Cat> Handle(UpdateCatByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine("Handling UpdateCatByIdCommand for Cat ID: {request.AnimalId}");

                var catToUpdate = await _animalRepository.GetByIdAsync(request.AnimalId) as Cat;

                if (catToUpdate != null)
                {
                    catToUpdate.Name = request.UpdatedCat.Name;
                    catToUpdate.BreedCat = request.UpdatedCat.BreedCat;
                    catToUpdate.WeightCat = request.UpdatedCat.WeightCat;

                    await _animalRepository.UpdateAsync(catToUpdate);

                    Console.WriteLine("Updated Cat with ID: {request.AnimalId}");

                    return catToUpdate;
                }
                else
                {
                    Console.WriteLine("Cat with ID {request.AnimalId} not found.");
                    throw new InvalidOperationException($"Cat with ID {request.AnimalId} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error handling UpdateCatByIdCommand: {ex.Message}");
                throw;
            }
        }
    }
}