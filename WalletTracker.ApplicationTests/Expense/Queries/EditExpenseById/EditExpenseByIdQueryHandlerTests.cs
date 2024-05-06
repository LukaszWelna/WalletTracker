using AutoMapper;
using FluentAssertions;
using Moq;
using WalletTracker.Application.Expense.Commands.EditExpenseById;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Expense.Queries.EditExpenseById.Tests
{
    public class EditExpenseByIdQueryHandlerTests
    {
        [Fact()]
        public async Task Handle_ForGivenId_ReturnCommand()
        {
            // Arrange 
            var query = new EditExpenseByIdQuery(1);

            var expense = new Domain.Entities.Expense()
            {
                Id = 1,
                CategoryId = 1,
                PaymentId = 1,
                Amount = 100,
                ExpenseDate = DateOnly.FromDateTime(DateTime.UtcNow),
                CreatedAt = DateTime.UtcNow,
                Comment = "Comment"
            };

            var command = new EditExpenseByIdCommand()
            {
                Id = 1,
                CategoryId = 1,
                PaymentId = 1,
                Amount = 100,
                ExpenseDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Comment = "Comment"
            };

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

            // Mock Expense repository
            var expenseRepositoryMock = new Mock<IExpenseRepository>();

            expenseRepositoryMock.Setup(e => e.GetExpenseById(query.ExpenseId))
                .ReturnsAsync(expense);

            // Mock Expense category repository
            var expenseCategoryRepositoryMock = new Mock<IExpenseCategoryRepository>();

            expenseCategoryRepositoryMock.Setup(e => e.GetCategoriesAssignedToLoggedUser())
                .ReturnsAsync(categoriesAssignedToUser);

            // Mock Payment method repository
            var paymentMethodRepositoryMock = new Mock<IPaymentMethodRepository>();

            paymentMethodRepositoryMock.Setup(p => p.GetPaymentMethodsAssignedToLoggedUser())
                .ReturnsAsync(paymentMethodsAssignedToUser);

            // Mock mapper
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<EditExpenseByIdCommand>(expense))
                .Returns(command);

            mapperMock.Setup(m => m.Map<List<ExpenseCategoryAssignedToUserDto>>(categoriesAssignedToUser))
                .Returns(categoryAssignedToUserDtos);

            mapperMock.Setup(m => m.Map<List<PaymentMethodAssignedToUserDto>>(paymentMethodsAssignedToUser))
                .Returns(paymentMethodAssignedToUserDtos);

            var handler = new EditExpenseByIdQueryHandler(expenseRepositoryMock.Object,
                expenseCategoryRepositoryMock.Object,
                paymentMethodRepositoryMock.Object,
                mapperMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(command);
            result.UserCategoryDtos.Should().BeEquivalentTo(categoryAssignedToUserDtos);
            result.UserPaymentMethodDtos.Should().BeEquivalentTo(paymentMethodAssignedToUserDtos);
        }
    }
}