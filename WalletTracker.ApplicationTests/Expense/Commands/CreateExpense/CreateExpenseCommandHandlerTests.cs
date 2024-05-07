using AutoMapper;
using FluentAssertions;
using Moq;
using WalletTracker.Application.ApplicationUser;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Expense.Commands.CreateExpense.Tests
{
    public class CreateExpenseCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidCommand_ShouldCreateExpense()
        {
            // Arrange
            var command = new CreateExpenseCommand()
            {
                Id = 1,
                CategoryId = 1,
                PaymentId = 1,
                Amount = 100,
                ExpenseDate = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            var expense = new Domain.Entities.Expense()
            {
                Id = 1,
                CategoryId = 1,
                PaymentId = 1,
                Amount = 100,
                ExpenseDate = DateOnly.FromDateTime(DateTime.UtcNow),
                CreatedAt = DateTime.UtcNow
            };

            var user = new CurrentUser("1", "test@test.com");

            // Mock mapper
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<Domain.Entities.Expense>(It.IsAny<CreateExpenseCommand>()))
                .Returns(expense);

            // Mock User context service
            var userContextServiceMock = new Mock<IUserContextService>();

            userContextServiceMock.Setup(u => u.GetCurrentUser())
                .Returns(user);

            // Mock Expense repository
            var expenseRepositoryMock = new Mock<IExpenseRepository>();

            // Create handler
            var handler = new CreateExpenseCommandHandler(expenseRepositoryMock.Object, mapperMock.Object, userContextServiceMock.Object);

            // Act 
            await handler.Handle(command, CancellationToken.None);

            // Assert
            expenseRepositoryMock.Verify(e => e.Create(expense), Times.Once);
            expense.UserId.Should().Be(user.Id);
        }
    }
}