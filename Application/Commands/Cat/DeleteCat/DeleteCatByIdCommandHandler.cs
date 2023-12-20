using MediatR;
using Domain.Models.Animal;
using Infrastructure.DataDbContex.Interfaces;

namespace Application.Commands.Cats.DeleteCat
{
    public class DeleteCatByIdCommandHandler : IRequestHandler<DeleteCatByIdCommand, bool>
    {
        private readonly IAnimalsRepository _animalsRepository;

        public DeleteCatByIdCommandHandler(IAnimalsRepository animalsRepository)
        {
            _animalsRepository = animalsRepository;
        }

        public async Task<bool> Handle(DeleteCatByIdCommand request, CancellationToken cancellationToken)
        {
            AnimalModel catToDelete = await _animalsRepository.GetByIdAsync(request.DeletedCatId);

            if (catToDelete == null)
            {
                return false;
            }

            await _animalsRepository.DeleteAsync(request.DeletedCatId);
            return true;
        }
    }
}
