using Domain.Models;
using Application.Queries.Cats.GetAll;
using Infrastructure.Database;

namespace Test.CatTests.QueryTest
{
    [TestFixture]
    public class GetAllCatsTests
	{
        private GetAllCatsQueryHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new GetAllCatsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task IfAllCatsReturnsCorrect()
        {
            var query = new GetAllCatsQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<Cat>>());
            Assert.That(result.Count, Is.GreaterThan(0));
        }
    }
}
