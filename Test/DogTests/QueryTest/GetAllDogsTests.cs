using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.Dogs;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.Database;

namespace Test.DogTests.QueryTest
{
    [TestFixture]
    public class GetAllDogsTests
    {
        private GetAllDogsQueryHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new GetAllDogsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task IfAllDogsReturnsCorrect()
        {
            var query = new GetAllDogsQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.That(result, Is.Not.Null); 
            Assert.That(result, Is.InstanceOf<List<Dog>>());  
            Assert.That(result.Count, Is.GreaterThan(0)); 
        }
    }
}
