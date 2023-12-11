using Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogByIdCommandHandler : IRequestHandler<DeleteDogByIdCommand, bool>
    {
        private readonly IConfiguration _configuration;

        public DeleteDogByIdCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> Handle(DeleteDogByIdCommand request, CancellationToken cancellationToken)
        {
            using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;

                    // Create an SQL query to delete the dog from your table
                    command.CommandText = "DELETE FROM Dog WHERE Dog_id = @Id";

                    // Create a parameter to avoid SQL injection
                    command.Parameters.AddWithValue("@Id", request.DeletedDogId);

                    // Execute the SQL query
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    // Return true if at least one row was affected (i.e., the dog was deleted), otherwise false
                    return rowsAffected > 0;
                }
            }
        }
    }
}
