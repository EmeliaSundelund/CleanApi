using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.Birds.GetById;
using Infrastructure.Database;

namespace Test.BirdTests.QueryTest
{
    [TestFixture]
    public class GetBirdByIdTests
    {
        private GetBirdByIdQueryHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new GetBirdByIdQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task ReturnBirdIdIfCorrect()
        {
            var birdId = new Guid("b8c746d3-aa71-4f11-bf8e-7cfc30a890a2");

            var query = new GetBirdByIdQuery(birdId);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(birdId));
        }

    }
}
