using MediatR;
using Domain.Models.Animal;
using Infrastructure.DataDbContex.Interfaces;
using Microsoft.Extensions.Logging; 

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
            try
            {
               Console.WriteLine("Handling DeleteCatByIdCommand for Cat ID: {request.DeletedCatId}");

                AnimalModel catToDelete = await _animalsRepository.GetByIdAsync(request.DeletedCatId);

                if (catToDelete == null)
                {
                    Console.WriteLine("Cat with ID {request.DeletedCatId} not found.");
                    return false;
                }

                await _animalsRepository.DeleteAsync(request.DeletedCatId);
                Console.WriteLine("Deleted Cat with ID {request.DeletedCatId}.");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error handling DeleteCatByIdCommand: {ex.Message}");
                throw;
            }
        }
    }
}