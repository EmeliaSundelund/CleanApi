using System;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Domain.Models.Person;
using Infrastructure.DataDbContex;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;

namespace Application.Queries.Users.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserModel>>
    {

        private readonly IUserInterface _userInterface;

        public GetAllUsersQueryHandler(IUserInterface userInterface)

        {

            _userInterface = userInterface;

        }

        public async Task<List<UserModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)

        {

            List<UserModel> allUser = await _userInterface.GetAllUsersAsync();

            return allUser;

        }

    }


}
