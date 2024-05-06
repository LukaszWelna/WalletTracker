using Xunit;
using WalletTracker.Application.Settings.Queries.GetExpenseCategoryFormToEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using WalletTracker.Application.Expense;
using WalletTracker.Application.Settings.Queries.GetExpenseCategoryFormToDelete;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using FluentAssertions;

namespace WalletTracker.Application.Settings.Queries.GetExpenseCategoryFormToEdit.Tests
{
    public class GetExpenseCategoryFormToEditQueryHandlerTests
    {
        [Fact()]
        public async Task Handle_ReturnCommand()
        {
            // Arrange
            var query = new GetExpenseCategoryFormToEditQuery();

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

            expenseCategoryRepositoryMock.Setup(i => i.GetCategoriesAssignedToLoggedUser())
                .ReturnsAsync(categoriesAssignedToUser);

            // Mock mapper
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<List<ExpenseCategoryAssignedToUserDto>>(categoriesAssignedToUser))
                .Returns(categoryAssignedToUserDtos);

            var handler = new GetExpenseCategoryFormToEditQueryHandler(expenseCategoryRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.UserCategoryDtos.Should().BeEquivalentTo(categoryAssignedToUserDtos);
        }
    }
}