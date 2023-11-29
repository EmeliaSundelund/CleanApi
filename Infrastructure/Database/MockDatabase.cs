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
            new Cat { Id = Guid.NewGuid(), Name = "Maja", LikesToPlay = true },
            new Cat { Id = Guid.NewGuid(), Name = "Oscar", LikesToPlay = false },
            new Cat { Id = Guid.NewGuid(), Name = "Viktor", LikesToPlay = false },
            new Cat { Id = Guid.NewGuid(), Name = "Julia", LikesToPlay = true },
            new Cat { Id = Guid.NewGuid(), Name = "Joel", LikesToPlay = false },
            new Cat { Id = new Guid("d6a8f7b4-3c9e-4a72-815d-9f25c6e8b051"), Name = "TestCatForUnitTests"}
        };

        public List<Bird> Birds
        {
            get { return allBirds; }
            set { allBirds = value; }
        }

        private static List<Bird> allBirds = new()
        {
            new Bird { Id = Guid.NewGuid(), Name = "Simba", CanFly = true },
            new Bird { Id = Guid.NewGuid(), Name = "Scar", CanFly = true },
            new Bird { Id = Guid.NewGuid(), Name = "Timon", CanFly = true },
            new Bird { Id = Guid.NewGuid(), Name = "Pumbaa", CanFly = false },
            new Bird { Id = Guid.NewGuid(), Name = "Zazu", CanFly = true },
            new Bird { Id = new Guid("b8c746d3-aa71-4f11-bf8e-7cfc30a890a2"), Name = "TestBirdForUnitTests"}
        };
    }
}
