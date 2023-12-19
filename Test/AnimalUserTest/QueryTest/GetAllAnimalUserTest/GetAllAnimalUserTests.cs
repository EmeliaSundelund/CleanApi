using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Queries.AnimalUser.GetAllAnimalUser;
using Domain.Models.AnimalUser;
using Infrastructure.DataDbContex.Interfaces;
using Moq;
using NUnit.Framework;

namespace Test.AnimalUserTest.QueryTest.GetAllAnimalUserTests
{
    [TestFixture]
    public class GetAllAnimalUsersQueryHandlerTests
    {
        [Test]
        public async Task Handle_ShouldReturnListOfAnimalUserDtos_WhenRepositoryHasData()
        {
            // Arrange
            var mockRepository = new Mock<IAnimalUserRepository>();
            var handler = new GetAllAnimalUsersQueryHandler(mockRepository.Object);

            // Mock the repository method to return a list of AnimalUserModel
            var animalUsersData = new List<AnimalUserModel>
            {
                new AnimalUserModel { UserId = Guid.NewGuid(), AnimalId = Guid.NewGuid() },
                new AnimalUserModel { UserId = Guid.NewGuid(), AnimalId = Guid.NewGuid() },
                // Add more test data as needed
            };

            mockRepository.Setup(repo => repo.GetAllAnimalUsersAsync())
                          .ReturnsAsync(animalUsersData);

            var query = new GetAllAnimalUsersQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<AnimalUserDto>>(result);
            Assert.AreEqual(animalUsersData.Count, result.Count);

            // Add more specific assertions based on your application logic
            // You may want to compare individual properties to ensure the mapping is correct
            // For example: Assert.AreEqual(animalUsersData[0].UserId, result[0].UserId);
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyList_WhenRepositoryHasNoData()
        {
            // Arrange
            var mockRepository = new Mock<IAnimalUserRepository>();
            var handler = new GetAllAnimalUsersQueryHandler(mockRepository.Object);

            // Mock the repository method to return an empty list
            mockRepository.Setup(repo => repo.GetAllAnimalUsersAsync())
                          .ReturnsAsync(new List<AnimalUserModel>());

            var query = new GetAllAnimalUsersQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<AnimalUserDto>>(result);
            Assert.IsEmpty(result);

            // Add more specific assertions based on your application logic
        }
    }
}
