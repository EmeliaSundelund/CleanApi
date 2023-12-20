﻿using MediatR;
using Domain.Models.Animal;
using Infrastructure.DataDbContex.Interfaces;

namespace Application.Commands.Birds.DeleteBird
{
    public class DeleteBirdByIdCommandHandler : IRequestHandler<DeleteBirdByIdCommand, bool>
    {
        private readonly IAnimalsRepository _animalsReprository;

        public DeleteBirdByIdCommandHandler(IAnimalsRepository animalRepository)
        {
            _animalsReprository = animalRepository;
        }

        public async Task<bool> Handle(DeleteBirdByIdCommand request, CancellationToken cancellationToken)
        {
            AnimalModel birdToDelete = await _animalsReprository.GetByIdAsync(request.DeletedBirdId);

            if (birdToDelete == null)
            {
                return false;
            }

            await _animalsReprository.DeleteAsync(request.DeletedBirdId);
            return true;
        }
    }
}
