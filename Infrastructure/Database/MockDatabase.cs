using Domain.Models;
using Infrastructure.DataDbContex;

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
            new Dog { AnimalId = Guid.NewGuid(), Name = "Björn"},
            new Dog { AnimalId = Guid.NewGuid(), Name = "Patrik"},
            new Dog { AnimalId = Guid.NewGuid(), Name = "Alfred"},
            new Dog { AnimalId = new Guid("12345678-1234-5678-1234-567812345678"), Name = "TestDogForUnitTests"}
        };

        public List<Cat> Cats
        {
            get { return allCats; }
            set { allCats = value; }
        }

        private static List<Cat> allCats = new()
        {
            new Cat { AnimalId = Guid.NewGuid(), Name = "Maja", LikesToPlay = true },
            new Cat { AnimalId = Guid.NewGuid(), Name = "Oscar", LikesToPlay = false },
            new Cat { AnimalId = Guid.NewGuid(), Name = "Viktor", LikesToPlay = false },
            new Cat { AnimalId = Guid.NewGuid(), Name = "Julia", LikesToPlay = true },
            new Cat { AnimalId = Guid.NewGuid(), Name = "Joel", LikesToPlay = false },
            new Cat { AnimalId = new Guid("d6a8f7b4-3c9e-4a72-815d-9f25c6e8b051"), Name = "TestCatForUnitTests"}
        };

        public List<Bird> Birds
        {
            get { return allBirds; }
            set { allBirds = value; }
        }

        private static List<Bird> allBirds = new()
        {
            new Bird { AnimalId = Guid.NewGuid(), Name = "Simba", CanFly = true },
            new Bird { AnimalId = Guid.NewGuid(), Name = "Scar", CanFly = true },
            new Bird { AnimalId = Guid.NewGuid(), Name = "Timon", CanFly = true },
            new Bird { AnimalId = Guid.NewGuid(), Name = "Pumbaa", CanFly = false },
            new Bird { AnimalId = Guid.NewGuid(), Name = "Zazu", CanFly = true },
            new Bird { AnimalId = new Guid("b8c746d3-aa71-4f11-bf8e-7cfc30a890a2"), Name = "TestBirdForUnitTests"}
        };
    }

}
