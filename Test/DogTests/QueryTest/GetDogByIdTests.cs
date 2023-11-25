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
        public async Task ReturnDogIdIfCorrekt()
        {
            var dogId = new Guid("12345678-1234-5678-1234-567812345678");

            var query = new GetDogByIdQuery(dogId);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.That(result.Id, Is.EqualTo(dogId));
        }

    }
}
