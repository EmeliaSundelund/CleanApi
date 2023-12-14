using System;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.DataDbContex;
using MediatR;

namespace Application.Queries.Users.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserS>>
    {

        private readonly UserInterface _userInterface;

        public GetAllUsersQueryHandler(UserInterface userInterface)

        {

            _userInterface = userInterface;

        }

        public async Task<List<UserS>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)

        {

            List<UserS> allUser = await _userInterface.GetAllUsersAsync();

            return allUser;

        }

    }


}
