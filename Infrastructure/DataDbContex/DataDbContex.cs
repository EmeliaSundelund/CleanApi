using Domain.Models;
using Domain.Models.Animal;
using Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DataDbContex
{
	public class DataDbContex : DbContext
    {
        public DbSet<AnimalModel> Animals { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Bird> Birds { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Cat> Cats { get; set; }

      
        public DataDbContex(DbContextOptions<DataDbContex> options)
            : base(options)
        {
            
            Database.EnsureCreated();
        }
    }
}

