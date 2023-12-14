using Domain.Models;
using Infrastructure.DataDbContex;
using MediatR;

namespace Application.Queries.Users.GetById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserS>
    {
        private readonly UserInterface _userInterface;

        public GetUserByIdQueryHandler(UserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public async Task<UserS> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            UserS wantedUser = await _userInterface.GetByIdAsync(request.Id) as UserS;

            return wantedUser;
        }
    }
}
