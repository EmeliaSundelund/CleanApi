using Infrastructure.Database;
using Infrastructure.DataDbContex;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<Database.MockDatabase>();
            services.AddScoped<IAnimalsRepository, AnimalsRepository>();
            return services;
        }
    }
}
