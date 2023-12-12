using Domain.Models;
using Infrastructure.Database;
using MediatR;
using Infrastructure.DataDbContex;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

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
            var catToUpdate = await _animalRepository.GetByIdAsync(request.Id) as Cat;

            if (catToUpdate != null)
            {
                catToUpdate.Name = request.UpdatedCat.Name;
                catToUpdate.BreedCat = request.UpdatedCat.BreedCat;
                catToUpdate.WeightCat = request.UpdatedCat.WeightCat;

                await _animalRepository.UpdateAsync(catToUpdate);

                return catToUpdate;
            }else
            {
                throw new InvalidOperationException($"Cat with ID {request.Id} not found.");
            }
        }
    }
}
