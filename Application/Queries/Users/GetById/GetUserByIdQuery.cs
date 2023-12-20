using Domain.Models;
using Domain.Models.Person;
using MediatR;

namespace Application.Queries.Users.GetById
{
    public class GetUserByIdQuery : IRequest<UserModel>
    {
        public GetUserByIdQuery(Guid userId)
        {
            Id = userId;
        }

        public Guid Id { get; }
    }
}
