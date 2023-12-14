using Application.Commands.Dogs.DeleteDog;
using Domain.Models;
using Infrastructure.DataDbContex;
using MediatR;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Infrastructure.Database;
using Domain.Models.Animal;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogByIdCommandHandler : IRequestHandler<DeleteDogByIdCommand, bool>
    {
        private readonly IAnimalsRepository _animalsRepository;

        public DeleteDogByIdCommandHandler(IAnimalsRepository animalsRepository)
        {
            _animalsRepository = animalsRepository;
        }

        public async Task<bool> Handle(DeleteDogByIdCommand request, CancellationToken cancellationToken)
        {
            AnimalModel dogToDelete = await _animalsRepository.GetByIdAsync(request.DeletedDogId);

            if (dogToDelete == null)
            {
                return false;
            }

            await _animalsRepository.DeleteAsync(request.DeletedDogId);
            return true;
        }
    }
}

