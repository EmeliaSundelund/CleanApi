using Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Application.Commands.Dogs.UpdateDog
{
    public class UpdateDogByIdCommandHandler : IRequestHandler<UpdateDogByIdCommand, Dog>
    {
        private readonly IConfiguration _configuration;

        public UpdateDogByIdCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Dog> Handle(UpdateDogByIdCommand request, CancellationToken cancellationToken)
        {
            using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;

                    // Create an SQL query to update the dog in your table
                    command.CommandText = "UPDATE Dog SET Dog_name = @Name, Dog_breed = @Breed, Dog_weight = @Weight WHERE Dog_id = @Id";

                    // Create parameters to avoid SQL injection
                    command.Parameters.AddWithValue("@Id", request.Id);
                    command.Parameters.AddWithValue("@Name", request.UpdatedDog.Name);
                    command.Parameters.AddWithValue("@Breed", request.UpdatedDog.BreedDog);
                    command.Parameters.AddWithValue("@Weight", request.UpdatedDog.WeightDog);

                    // Execute the SQL query
                    await command.ExecuteNonQueryAsync();
                }
            }

            // Return the updated dog
            return new Dog
            {
                id = request.Id,
                Name = request.UpdatedDog.Name,
                BreedDog = request.UpdatedDog.BreedDog,
                WeightDog = request.UpdatedDog.WeightDog
            };
        }
    }
}