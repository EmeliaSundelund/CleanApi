using MediatR;
using Domain.Models.Animal;
using Infrastructure.DataDbContex.Interfaces;
using Microsoft.Extensions.Logging; 

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogByIdCommandHandler : IRequestHandler<DeleteDogByIdCommand, bool>
    {
        private readonly IAnimalsRepository _animalsRepository;

        public DeleteDogByIdCommandHandler(IAnimalsRepository animalsRepository)
        {
            _animalsRepository = animalsRepository;
        }

        public async Task<bool> Handle(DeleteDogByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine("Handling DeleteDogByIdCommand for Dog ID: {request.DeletedDogId}");

                AnimalModel dogToDelete = await _animalsRepository.GetByIdAsync(request.DeletedDogId);

                if (dogToDelete == null)
                {
                    Console.WriteLine("Dog with ID {request.DeletedDogId} not found.");
                    return false;
                }

                await _animalsRepository.DeleteAsync(request.DeletedDogId);
                Console.WriteLine("Deleted Dog with ID: {request.DeletedDogId}");

                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error handling DeleteDogByIdCommand: {ex.Message}");
                throw;
            }
        }
    }
}