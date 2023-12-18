﻿using Domain.Models;
using Domain.Models.Person;
using Infrastructure.DataDbContex;
using MediatR;

namespace Application.Queries.Users.GetById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserModel>
    {
        private readonly UserInterface _userInterface;

        public GetUserByIdQueryHandler(UserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public async Task<UserModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            UserModel wantedUser = await _userInterface.GetByIdAsync(request.Id) as UserModel;

            return wantedUser;
        }
    }
}
