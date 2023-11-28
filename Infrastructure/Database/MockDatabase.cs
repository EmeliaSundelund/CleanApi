using Domain.Models;

namespace Infrastructure.Database
{
    public class MockDatabase
    {
        public List<Dog> Dogs
        {
            get { return allDogs; }
            set { allDogs = value; }
        }
        //kommentar
        private static List<Dog> allDogs = new()
        {
            new Dog { Id = Guid.NewGuid(), Name = "Björn"},
            new Dog { Id = Guid.NewGuid(), Name = "Patrik"},
            new Dog { Id = Guid.NewGuid(), Name = "Alfred"},
            new Dog { Id = new Guid("12345678-1234-5678-1234-567812345678"), Name = "TestDogForUnitTests"}
        };

        public List<Cat> Cats
        {
            get { return allCats; }
            set { allCats = value; }
        }

        private static List<Cat> allCats = new()
        {
            new Cat { Id = Guid.NewGuid(), Name = "Alex", LikesToPlay = true },
            new Cat { Id = Guid.NewGuid(), Name = "Emma", LikesToPlay = false },
            new Cat { Id = Guid.NewGuid(), Name = "Maja", LikesToPlay = false },
            new Cat { Id = Guid.NewGuid(), Name = "Felix", LikesToPlay = true },
            new Cat { Id = Guid.NewGuid(), Name = "Albin", LikesToPlay = false },
            new Cat { Id = new Guid("f13b2a88-6a1e-4d05-9c71-c2514efc89d3"), Name = "TestCatForUnitTests"}
        };
    }
}