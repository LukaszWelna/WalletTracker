using AutoMapper;
using FluentAssertions;
using Moq;
using WalletTracker.Application.Expense;
using WalletTracker.Application.Income;
using WalletTracker.Domain.Interfaces;
using WalletTracker.Domain.Models;
using Xunit;

namespace WalletTracker.Application.Balance.Queries.GetBalanceData.Tests
{
    public class GetBalanceDataQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ForValidStartAndEndDates_ShouldReturnBalanceDto()
        {
            // Arrange
            var query = new GetBalanceDataQuery()
            {
                StartDate = DateOnly.FromDateTime(new DateTime(2024, 05, 01)),
                EndDate = DateOnly.FromDateTime(new DateTime(2024, 05, 07))
            };

            var userIncomes = new List<List<Domain.Entities.Income>>()
            {
                new List<Domain.Entities.Income>()
                {
                    new Domain.Entities.Income()
                    {
                        Id = 1,
                        UserId = "2",
                        CategoryId = 1,
                        Amount = 100,
                        IncomeDate = DateOnly.FromDateTime(new DateTime(2024, 05, 05)),
                        CreatedAt = new DateTime(2024, 05, 05),
                        Comment = "Test income comment"
                    }
                }
            };

            var userIncomeDtos = new List<List<GetIncomeDto>>()
            {
                new List<GetIncomeDto>()
                {
                    new GetIncomeDto()
                    {
                        Id = 1,
                        Amount = 100,
                        IncomeDate = DateOnly.FromDateTime(new DateTime(2024, 05, 05)),
                        CategoryName = "Test income category name",
                        Comment = "Test income comment"
                    }
                }
            };

            var userExpenses = new List<List<Domain.Entities.Expense>>()
            {
                new List<Domain.Entities.Expense>()
                {
                    new Domain.Entities.Expense()
                    {
                        Id = 1,
                        UserId = "2",
                        CategoryId = 1,
                        PaymentId = 1,
                        Amount = 100,
                        ExpenseDate = DateOnly.FromDateTime(new DateTime(2024, 05, 05)),
                        CreatedAt = new DateTime(2024, 05, 05),
                        Comment = "Test expense comment"
                    }
                }
            };

            var userExpenseDtos = new List<List<GetExpenseDto>>()
            {
                new List<GetExpenseDto>()
                {
                    new GetExpenseDto()
                    {
                        Id = 1,
                        Amount = 100,
                        ExpenseDate = DateOnly.FromDateTime(new DateTime(2024, 05, 05)),
                        CategoryName = "Test income category name",
                        PaymentName = "Test payment method name",
                        Comment = "Test income comment"
                    }
                }
            };

            var incomeTotalAmountInCategories = new List<IncomeTotalAmountInCategoryDto>()
            {
                new IncomeTotalAmountInCategoryDto()
                {
                    CategoryName = "Test income category name",
                    TotalAmount = 100
                }
            };

            var expenseTotalAmountInCategories = new List<ExpenseTotalAmountInCategoryDto>()
            {
                new ExpenseTotalAmountInCategoryDto()
                {
                    CategoryName = "Test expense category name",
                    TotalAmount = 100
                }
            };

            var balanceCanvasDtos = new List<BalanceCanvasDto>()
            {
                new BalanceCanvasDto()
                {
                    label = "Test expense category name",
                    y = 100
                }
            };

            var balanceDto = new BalanceDto()
            {
                Incomes = userIncomeDtos,
                Expenses = userExpenseDtos,
                IncomeTotalAmountInCategories = incomeTotalAmountInCategories,
                ExpenseTotalAmountInCategories = expenseTotalAmountInCategories,
                TotalIncomesAmount = 100,
                TotalExpensesAmount = 100,
                BalanceCanvasDtos = balanceCanvasDtos,
                StartDate = query.StartDate,
                EndDate = query.EndDate
            };

            // Mock mapper
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<List<List<GetIncomeDto>>>(It.IsAny<List<List<Domain.Entities.Income>>>()))
                .Returns(userIncomeDtos);

            mapperMock.Setup(m => m.Map<List<List<GetExpenseDto>>>(It.IsAny<List<List<Domain.Entities.Expense>>>()))
                .Returns(userExpenseDtos);

            mapperMock.Setup(m => m.Map<List<BalanceCanvasDto>>(It.IsAny<List<ExpenseTotalAmountInCategoryDto>>()))
                .Returns(balanceCanvasDtos);

            // Mock Income repository
            var incomeRepositoryMock = new Mock<IIncomeRepository>();

            incomeRepositoryMock.Setup(i => i.GetUserIncomesFromPeriod(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()))
                .ReturnsAsync(userIncomes);

            incomeRepositoryMock.Setup(i => i.GetTotalAmountInCategories(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()))
                .ReturnsAsync(incomeTotalAmountInCategories);

            // Mock Expense repository
            var expenseRepositoryMock = new Mock<IExpenseRepository>();

            expenseRepositoryMock.Setup(e => e.GetUserExpensesFromPeriod(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()))
                .ReturnsAsync(userExpenses);

            expenseRepositoryMock.Setup(e => e.GetTotalAmountInCategories(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()))
                .ReturnsAsync(expenseTotalAmountInCategories);

            var handler = new GetBalanceDataQueryHandler(incomeRepositoryMock.Object,
                expenseRepositoryMock.Object,
                mapperMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(balanceDto);
            incomeRepositoryMock.Verify(i => i.GetUserIncomesFromPeriod(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()), Times.Once);
            expenseRepositoryMock.Verify(e => e.GetUserExpensesFromPeriod(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()), Times.Once);
            incomeRepositoryMock.Verify(i => i.GetTotalAmountInCategories(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()), Times.Once);
            expenseRepositoryMock.Verify(e => e.GetTotalAmountInCategories(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()), Times.Once);
        }
    }
}