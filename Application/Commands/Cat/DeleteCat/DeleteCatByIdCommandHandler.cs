using Application.Commands.Cats.DeleteCat;
using Domain.Models;
using Infrastructure.DataDbContex;
using MediatR;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Infrastructure.Database;
using Domain.Models.Animal;
using Infrastructure.DataDbContex.Interfaces;

namespace Application.Commands.Cats.DeleteCat
{
    public class DeleteCatByIdCommandHandler : IRequestHandler<DeleteCatByIdCommand, bool>
    {
        private readonly IAnimalsRepository _animalsRepository;

        public DeleteCatByIdCommandHandler(IAnimalsRepository animalsRepository)
        {
            _animalsRepository = animalsRepository;
        }

        public async Task<bool> Handle(DeleteCatByIdCommand request, CancellationToken cancellationToken)
        {
            AnimalModel catToDelete = await _animalsRepository.GetByIdAsync(request.DeletedCatId);

            if (catToDelete == null)
            {
                return false;
            }

            await _animalsRepository.DeleteAsync(request.DeletedCatId);
            return true;
        }
    }
}
