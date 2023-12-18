using Application;
using Infrastructure;
using Infrastructure.DataDbContex;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddApplication().AddInfrastructure();

        // Add DbContext service
        builder.Services.AddDbContext<DataDbContex>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                b => b.MigrationsAssembly("Infrastructure"));
        });

        // Register UserInterface as a scoped service
        builder.Services.AddScoped<UserInterface, UsersRepository>();

        // Add AnimalUserRepository as a transient service
        builder.Services.AddTransient<IAnimalUserRepository, AnimalUserRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
