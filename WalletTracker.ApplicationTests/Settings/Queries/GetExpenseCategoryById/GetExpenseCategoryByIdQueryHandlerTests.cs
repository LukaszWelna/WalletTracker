using AutoMapper;
using FluentAssertions;
using Moq;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Settings.Queries.GetExpenseCategoryById.Tests
{
    public class GetExpenseCategoryByIdQueryHandlerTests
    {
        [Fact()]
        public async Task Handle_ForGivenId_ReturnExpenseCategorySettingDto()
        {
            // Arrange 
            var query = new GetExpenseCategoryByIdQuery(1);

            var expenseCategory = new ExpenseCategoryAssignedToUser()
            {
                Id = 1,
                Name = "Test category",
                Limit = 1000,
                LimitIsActive = true
            };

            var expenseCategorySettingDto = new ExpenseCategorySettingsDto()
            {
                Id = 1,
                Name = "Test category",
                Limit = 1000,
                LimitIsActive = true
            };

            // Mock Expense category repository
            var expenseCategoryRepositoryMock = new Mock<IExpenseCategoryRepository>();

            expenseCategoryRepositoryMock.Setup(e => e.GetById(It.IsAny<int>()))
                .ReturnsAsync(expenseCategory);

            // Mock mapper
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<ExpenseCategorySettingsDto>(expenseCategory))
                .Returns(expenseCategorySettingDto);

            var handler = new GetExpenseCategoryByIdQueryHandler(expenseCategoryRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expenseCategorySettingDto);
        }
    }
}