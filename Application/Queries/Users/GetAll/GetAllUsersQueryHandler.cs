using System;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Domain.Models.Person;
using Infrastructure.DataDbContex;
using MediatR;

namespace Application.Queries.Users.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserModel>>
    {

        private readonly UserInterface _userInterface;

        public GetAllUsersQueryHandler(UserInterface userInterface)

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
