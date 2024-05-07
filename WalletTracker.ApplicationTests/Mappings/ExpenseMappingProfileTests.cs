using AutoMapper;
using FluentAssertions;
using WalletTracker.Application.Expense;
using WalletTracker.Domain.Entities;
using Xunit;

namespace WalletTracker.Application.Mappings.Tests
{
    public class ExpenseMappingProfileTests
    {
        [Fact]
        public void ExpenseMappingProfile_ShouldMapExpenseToGetExpenseDto()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ExpenseMappingProfile>());

            var mapper = configuration.CreateMapper();

            var expense = new Domain.Entities.Expense()
            {
                Category = new ExpenseCategoryAssignedToUser()
                {
                    Name = "TestCategoryName"
                },
                Payment = new PaymentMethodAssignedToUser()
                {
                    Name = "TestPaymentMethod"
                }
            };

            // Act
            var result = mapper.Map<GetExpenseDto>(expense);

            // Assert
            result.Should().NotBeNull();
            result.CategoryName.Should().Be(expense.Category.Name);
            result.PaymentName.Should().Be(expense.Payment.Name);
        }
    }
}