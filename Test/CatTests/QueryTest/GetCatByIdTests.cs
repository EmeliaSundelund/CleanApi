using System;
using Application.Queries.Cats.GetById;
using Infrastructure.Database;

namespace Test.CatTests.QueryTest
{
    [TestFixture]
    public class GetCatByIdTests
    {

        private GetCatByIdQueryHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new GetCatByIdQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task ReturnCatIdIfCorrect()
        {
            //Arrange
            var catId = new Guid("d6a8f7b4-3c9e-4a72-815d-9f25c6e8b051");

            var query = new GetCatByIdQuery(catId);
            //Act
            var result = await _handler.Handle(query, CancellationToken.None);
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.id, Is.EqualTo(catId));
        }
    }
}