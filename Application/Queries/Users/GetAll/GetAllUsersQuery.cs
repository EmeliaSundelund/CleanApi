using Domain.Models;
using Domain.Models.Person;
using MediatR;

namespace Application.Queries.Users.GetAll
{
    public class GetAllUsersQuery : IRequest<List<UserModel>>
    {
    }
}