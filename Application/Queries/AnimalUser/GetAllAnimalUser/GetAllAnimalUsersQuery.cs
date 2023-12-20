using Application.Dtos;
using MediatR;

namespace Application.Queries.AnimalUser.GetAllAnimalUser
{
    public class GetAllAnimalUsersQuery : IRequest<List<AnimalUserDto>>
    {
    }
}
