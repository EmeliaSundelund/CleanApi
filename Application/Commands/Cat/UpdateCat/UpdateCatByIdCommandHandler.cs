﻿using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Cats.UpdateCat
{
    public class UpdateCatByIdCommandHandler : IRequestHandler<UpdateCatByIdCommand, Cat>
    {
        private readonly MockDatabase _mockDatabase;

        public UpdateCatByIdCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        } //kommentar
        public Task<Cat> Handle(UpdateCatByIdCommand request, CancellationToken cancellationToken)
        {
            Cat catToUpdate = _mockDatabase.Cats.FirstOrDefault(cat => cat.id == request.Id)!;

            catToUpdate.Name = request.UpdatedCat.Name;
            catToUpdate.LikesToPlay = request.UpdatedCat.LikesToPlay;

            return Task.FromResult(catToUpdate);
        }
    }
}