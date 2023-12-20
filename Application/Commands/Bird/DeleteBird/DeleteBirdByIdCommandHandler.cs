using Domain.Models.Animal;
using Infrastructure.DataDbContex.Interfaces;
using MediatR;

namespace Application.Commands.Birds.DeleteBird
{
    public class DeleteBirdByIdCommandHandler : IRequestHandler<DeleteBirdByIdCommand, bool>
    {
        private readonly IAnimalsRepository _animalsReprository;


        public DeleteBirdByIdCommandHandler(IAnimalsRepository animalRepository)
        {
            _animalsReprository = animalRepository;

        }

        public async Task<bool> Handle(DeleteBirdByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {

                AnimalModel birdToDelete = await _animalsReprository.GetByIdAsync(request.DeletedBirdId);

                if (birdToDelete == null)
                {
                    Console.WriteLine("Bird with ID {request.DeletedBirdId} not found.");
                    return false;
                }

                await _animalsReprository.DeleteAsync(request.DeletedBirdId);
                Console.WriteLine($"Deleted Bird with ID {request.DeletedBirdId}.");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error handling DeleteBirdByIdCommand: {ex.Message}");

                throw;
            }
        }
    }
}