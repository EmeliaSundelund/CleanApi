using Application.Queries.Dogs.GetAll;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Application.Queries.Dogs
{
    public class GetAllDogsQueryHandler : IRequestHandler<GetAllDogsQuery, List<Dog>>
    {
        private readonly IConfiguration _configuration;

        public GetAllDogsQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Dog>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                // Anpassa SQL-frågan beroende på ditt schemanamn och fält
                using (var command = new MySqlCommand("SELECT * FROM Dog", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                    {
                        List<Dog> allDogsFromDatabase = new List<Dog>();

                        while (await reader.ReadAsync(cancellationToken))
                        {
                            var dog = new Dog
                            {
                                // Anpassa detta beroende på din tabells struktur
                                id = Guid.Parse(reader.GetString(reader.GetOrdinal("Dog_id"))),
                                Name = reader.GetString(reader.GetOrdinal("Dog_name")),
                                BreedDog = reader.GetString(reader.GetOrdinal("Dog_breed")),
                                WeightDog = reader.GetInt32(reader.GetOrdinal("Dog_weight"))
                                // Lägg till resten av fälten här
                            };

                            allDogsFromDatabase.Add(dog);
                        }

                        return allDogsFromDatabase;
                    }
                }
            }
        }
    }
}
