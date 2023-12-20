using Application.Dtos;
using Domain.Models.AnimalUser;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;

namespace Application.Queries.AnimalUser.GetAllAnimalUser
{
    public class GetAllAnimalUsersQueryHandler : IRequestHandler<GetAllAnimalUsersQuery, List<AnimalUserDto>>
    {
        private readonly IAnimalUserRepository _animalUserRepository;
        public GetAllAnimalUsersQueryHandler(IAnimalUserRepository animalUserRepository)
        {
            _animalUserRepository = animalUserRepository;
        }

        public async Task<List<AnimalUserDto>> Handle(GetAllAnimalUsersQuery request, CancellationToken cancellationToken)
        {
            var animalUsers = await _animalUserRepository.GetAllAnimalUsersAsync();

            return animalUsers.Select(au => new AnimalUserDto
            {
                UserId = au.UserId,
                AnimalId = au.AnimalId,
            }).ToList();
        }
    }
}