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
            //Arrange
            var query = new GetAllCatsQuery();
            //Act
            var result = await _handler.Handle(query, CancellationToken.None);
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<Cat>>());
            Assert.That(result.Count, Is.GreaterThan(0));
        }
    }
}