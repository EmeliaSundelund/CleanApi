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
            var catId = new Guid("f13b2a88-6a1e-4d05-9c71-c2514efc89d3");

            var query = new GetCatByIdQuery(catId);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(catId));
        }
    }
}