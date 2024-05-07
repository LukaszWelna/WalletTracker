using FluentAssertions;
using Moq;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Settings.Queries.GetIncomeCategoryByName.Tests
{
    public class GetIncomeCategoryByNameQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ForGivenName_ShouldReturnIncomeCategoryAssignedToUser()
        {
            // Arrange 
            var query = new GetIncomeCategoryByNameQuery("Test name");

            var incomeCategory = new IncomeCategoryAssignedToUser()
            {
                Id = 1,
                Name = "Test category"
            };

            // Mock Income category repository
            var incomeCategoryRepositoryMock = new Mock<IIncomeCategoryRepository>();

            incomeCategoryRepositoryMock.Setup(i => i.GetByName(It.IsAny<string>()))
                .ReturnsAsync(incomeCategory);

            var handler = new GetIncomeCategoryByNameQueryHandler(incomeCategoryRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(incomeCategory);
        }
    }
}