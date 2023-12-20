using System;
using System.Collections.Generic;
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
            Assert.That(result, Is.Not.Null); // Använd Not.Null istället för IsNotNull
            Assert.That(result, Is.InstanceOf<List<AnimalUserDto>>());
            Assert.That(result.Count, Is.EqualTo(animalUsersData.Count));

            // Add more specific assertions based on your application logic
            // You may want to compare individual properties to ensure the mapping is correct
            // For example: Assert.That(result[0].UserId, Is.EqualTo(animalUsersData[0].UserId));
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
            Assert.That(result, Is.Not.Null); // Använd Not.Null istället för IsNotNull
            Assert.That(result, Is.InstanceOf<List<AnimalUserDto>>());
            Assert.That(result, Is.Empty);

            // Add more specific assertions based on your application logic
        }
    }
}