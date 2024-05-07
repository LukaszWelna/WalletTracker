using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Moq;
using System.Net;
using WalletTracker.Application.Balance;
using WalletTracker.Application.Balance.Queries.GetBalanceData;
using WalletTracker.Application.Expense;
using WalletTracker.Application.Income;
using WalletTracker.Domain.Models;
using WalletTracker.MVCTests;
using Xunit;

namespace WalletTracker.MVC.Controllers.Tests
{
    public class BalanceControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public BalanceControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Index_ForExistingAllBalanceDtoProperties_ShouldReturnViewWithData()
        {
            // Arrange
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
                StartDate = DateOnly.FromDateTime(new DateTime(2024, 05, 07)),
                EndDate = DateOnly.FromDateTime(new DateTime(2024, 05, 07))
            };

            // Mock Mediator
            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(m => m.Send(It.IsAny<GetBalanceDataQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(balanceDto);

            var client = _factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddScoped(_ => mediatorMock.Object);

                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                    }))
                .CreateClient();

            // Act
            var response = await client.GetAsync("/Balance/Index");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("Test income category name")
                .And.Contain("Test expense category name")
                .And.Contain("Summary")
                .And.Contain("Congratulations! You manage your budget well.")
                .And.Contain("Your expenses");
        }

        [Fact]
        public async Task Index_ForEmptyBalanceDto_ShouldReturnEmptyView()
        {
            // Arrange
            var balanceDto = new BalanceDto();

            // Mock Mediator
            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(m => m.Send(It.IsAny<GetBalanceDataQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(balanceDto);

            var client = _factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddScoped(_ => mediatorMock.Object);

                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                    }))
                .CreateClient();

            // Act
            var response = await client.GetAsync("/Balance/Index");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("No incomes to show.")
                .And.Contain("No expenses to show.")
                .And.NotContain("Your expenses");
        }
    }
}