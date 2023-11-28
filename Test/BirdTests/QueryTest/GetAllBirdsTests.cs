using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.Birds;
using Application.Queries.Birds.GetAll;
using Domain.Models;
using Infrastructure.Database;

namespace Test.BirdsTests.QueryTest
{
    [TestFixture]
    public class GetAllBirdsTests
    {
        private GetAllBirdsQueryHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new GetAllBirdsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task IfAllBirdsReturnsCorrect()
        {
            var query = new GetAllBirdsQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<Bird>>());
            Assert.That(result.Count, Is.GreaterThan(0));
        }
    }
}

