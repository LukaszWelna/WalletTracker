using AutoMapper;
using FluentAssertions;
using Moq;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Expense.Queries.GetCategoryLimitData.Tests
{
    public class GetCategoryLimitDataQueryHandlerTests
    {
        [Fact()]
        public async Task Handle_ForGivenCategoryId_ReturnCorrectCategoryLimitDto()
        {
            // Arrange
            var query = new GetCategoryLimitDataQuery(1);

            var category = new ExpenseCategoryAssignedToUser()
            {
                Id = 1,
                Name = "Food",
                Limit = 1000,
                LimitIsActive = true
            };

            var categoryLimitDto = new CategoryLimitDto()
            {
                Limit = 1000,
                LimitIsActive = true
            };

            // Mock Expense category repository
            var expenseCategoryRepositoryMock = new Mock<IExpenseCategoryRepository>();

            expenseCategoryRepositoryMock.Setup(e => e.GetById(It.IsAny<int>()))
                .ReturnsAsync(category);

            // Mock mapper
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<CategoryLimitDto>(category))
                .Returns(categoryLimitDto);

            var handler = new GetCategoryLimitDataQueryHandler(expenseCategoryRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(categoryLimitDto);
        }
    }
}