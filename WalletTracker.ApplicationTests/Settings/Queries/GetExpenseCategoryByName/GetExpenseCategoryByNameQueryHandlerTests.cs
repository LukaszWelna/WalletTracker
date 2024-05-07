using Xunit;
using WalletTracker.Application.Settings.Queries.GetExpenseCategoryByName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using WalletTracker.Application.Settings.Queries.GetExpenseCategoryById;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using FluentAssertions;

namespace WalletTracker.Application.Settings.Queries.GetExpenseCategoryByName.Tests
{
    public class GetExpenseCategoryByNameQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ForGivenName_ShouldReturnExpenseCategoryAssignedToUser()
        {
            // Arrange 
            var query = new GetExpenseCategoryByNameQuery("Test name");

            var expenseCategory = new ExpenseCategoryAssignedToUser()
            {
                Id = 1,
                Name = "Test category",
                Limit = 1000,
                LimitIsActive = true
            };

            // Mock Expense category repository
            var expenseCategoryRepositoryMock = new Mock<IExpenseCategoryRepository>();

            expenseCategoryRepositoryMock.Setup(e => e.GetByName(It.IsAny<string>()))
                .ReturnsAsync(expenseCategory);

            var handler = new GetExpenseCategoryByNameQueryHandler(expenseCategoryRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expenseCategory);
        }
    }
}