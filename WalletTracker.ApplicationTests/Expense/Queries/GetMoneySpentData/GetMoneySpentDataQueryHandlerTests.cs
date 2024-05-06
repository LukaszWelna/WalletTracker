﻿using Xunit;
using WalletTracker.Application.Expense.Queries.GetMoneySpentData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WalletTracker.Domain.Interfaces;
using FluentAssertions;

namespace WalletTracker.Application.Expense.Queries.GetMoneySpentData.Tests
{
    public class GetMoneySpentDataQueryHandlerTests
    {
        [Fact()]
        public async Task Handle_ForGivenCategoryIdAndDate_ReturnTotalSpentMoney()
        {
            // Arrange
            var date = DateOnly.FromDateTime(new DateTime(2024, 05, 06));
            int categoryId = 1;

            var query = new GetMoneySpentDataQuery(categoryId, date);

            decimal moneySpent = 1000;

            // Mock Expense repository
            var expenseRepositoryMock = new Mock<IExpenseRepository>();

            expenseRepositoryMock.Setup(e => e.GetMoneySpent(categoryId, 
                DateOnly.FromDateTime(new DateTime(2024, 05, 01)),
                DateOnly.FromDateTime(new DateTime(2024, 05, 31))))
                .Returns(moneySpent);

            var handler = new GetMoneySpentDataQueryHandler(expenseRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().Be(moneySpent);
        }
    }
}