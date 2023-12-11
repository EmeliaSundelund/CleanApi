using Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Application.Commands.Dogs
{
    public class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
    {
        private readonly IConfiguration _configuration;

        public AddDogCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
        {
            using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;

                    // Skapa en SQL-fråga för att lägga till hunden i din tabell
                    command.CommandText = "INSERT INTO Dog (Dog_id, Dog_name, Dog_breed, Dog_weight) VALUES (@Id, @Name, @Breed, @Weight)";

                    // Skapa parametrar för att undvika SQL-injektion
                    command.Parameters.AddWithValue("@Id", Guid.NewGuid());
                    command.Parameters.AddWithValue("@Name", request.NewDog.Name);
                    command.Parameters.AddWithValue("@Breed", request.NewDog.BreedDog);
                    command.Parameters.AddWithValue("@Weight", request.NewDog.WeightDog);

                    // Utför SQL-frågan
                    await command.ExecuteNonQueryAsync();
                }
            }

            // Återvänd den skapade hunden (du kan också hämta hunden från databasen om det finns fler åtgärder du vill utföra)
            return new Dog
            {
                id = Guid.NewGuid(),
                Name = request.NewDog.Name,
                BreedDog = request.NewDog.BreedDog,
                WeightDog = request.NewDog.WeightDog
            };
        }
    }
}
