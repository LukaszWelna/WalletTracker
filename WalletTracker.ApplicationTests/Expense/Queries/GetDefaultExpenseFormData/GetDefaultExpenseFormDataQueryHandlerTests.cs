using AutoMapper;
using FluentAssertions;
using Moq;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Expense.Queries.GetDefaultExpenseFormData.Tests
{
    public class GetDefaultExpenseFormDataQueryHandlerTests
    {
        [Fact()]
        public async Task Handle_ReturnDefaultCommand()
        {
            // Arrange
            var query = new GetDefaultExpenseFormDataQuery();

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

            var paymentMethodsAssignedToUser = new List<PaymentMethodAssignedToUser>()
            {
                new PaymentMethodAssignedToUser()
                {
                    Id = 1,
                    Name = "Cash"
                }
            };

            var paymentMethodAssignedToUserDtos = new List<PaymentMethodAssignedToUserDto>()
            {
                new PaymentMethodAssignedToUserDto()
                {
                    Id = 1,
                    Name = "Cash"
                }
            };

            // Mock Expense category repository
            var expenseCategoryRepositoryMock = new Mock<IExpenseCategoryRepository>();

            expenseCategoryRepositoryMock.Setup(i => i.GetCategoriesAssignedToLoggedUser())
                .ReturnsAsync(categoriesAssignedToUser);

            // Mock Payment method repository
            var paymentMethodRepositoryMock = new Mock<IPaymentMethodRepository>();

            paymentMethodRepositoryMock.Setup(p => p.GetPaymentMethodsAssignedToLoggedUser())
                .ReturnsAsync(paymentMethodsAssignedToUser);

            // Mock mapper
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<List<ExpenseCategoryAssignedToUserDto>>(categoriesAssignedToUser))
                .Returns(categoryAssignedToUserDtos);

            mapperMock.Setup(m => m.Map<List<PaymentMethodAssignedToUserDto>>(paymentMethodsAssignedToUser))
                .Returns(paymentMethodAssignedToUserDtos);

            var handler = new GetDefaultExpenseFormDataQueryHandler(expenseCategoryRepositoryMock.Object,
                paymentMethodRepositoryMock.Object,
                mapperMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ExpenseDate.Should().Be(DateOnly.FromDateTime(DateTime.UtcNow));
            result.UserCategoryDtos.Should().BeEquivalentTo(categoryAssignedToUserDtos);
            result.UserPaymentMethodDtos.Should().BeEquivalentTo(paymentMethodAssignedToUserDtos);
        }
    }
}