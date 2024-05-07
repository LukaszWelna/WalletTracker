using AutoMapper;
using Moq;
using WalletTracker.Application.Expense;
using WalletTracker.Application.Expense.Queries.GetDefaultExpenseFormData;
using WalletTracker.Application.Income.Queries.GetCategoriesAssignedToLoggedUser;
using WalletTracker.Application.Income;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;
using FluentAssertions;

namespace WalletTracker.Application.Settings.Queries.GetExpenseCategoryFormToDelete.Tests
{
    public class GetExpenseCategoryFormToDeleteQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnCommand()
        {
            // Arrange
            var query = new GetExpenseCategoryFormToDeleteQuery();

            var categoriesAssignedToUser = new List<ExpenseCategoryAssignedToUser>()
            {
                new ExpenseCategoryAssignedToUser()
                {
                    Id = 1,
                    Name = "Food"
                }
            };

            var categoryAssignedToUserDtos = new List<ExpenseCategoryAssignedToUserDto>()
            {
                new ExpenseCategoryAssignedToUserDto()
                {
                    Id = 1,
                    Name = "Food"
                }
            };

            // Mock Expense category repository
            var expenseCategoryRepositoryMock = new Mock<IExpenseCategoryRepository>();

            expenseCategoryRepositoryMock.Setup(e => e.GetCategoriesAssignedToLoggedUser())
                .ReturnsAsync(categoriesAssignedToUser);

            // Mock mapper
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<List<ExpenseCategoryAssignedToUserDto>>(categoriesAssignedToUser))
                .Returns(categoryAssignedToUserDtos);

            var handler = new GetExpenseCategoryFormToDeleteQueryHandler(expenseCategoryRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.UserCategoryDtos.Should().BeEquivalentTo(categoryAssignedToUserDtos);
        }
    }
}