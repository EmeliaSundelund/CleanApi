using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.Dogs.GetById;
using Infrastructure.Database;

namespace Test.DogTests.QueryTest
{
    [TestFixture]
    public class GetDogByIdTests
    {
        private GetDogByIdQueryHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new GetDogByIdQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task ReturnDogIdIfCorrect()
        {
            // Arrange
            var dogId = new Guid("12345678-1234-5678-1234-567812345678");

            var query = new GetDogByIdQuery(dogId);
            //Act
            var result = await _handler.Handle(query, CancellationToken.None);
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.id, Is.EqualTo(dogId));
        }

    }
}
