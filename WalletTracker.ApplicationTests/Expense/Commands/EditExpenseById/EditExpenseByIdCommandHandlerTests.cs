using FluentAssertions;
using Moq;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Expense.Commands.EditExpenseById.Tests
{
    public class EditExpenseByIdCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidCommand_ShouldEditExpenseById()
        {
            // Arrange
            var command = new EditExpenseByIdCommand()
            {
                Id = 1,
                CategoryId = 2,
                PaymentId = 2,
                Amount = 1000,
                ExpenseDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-1),
                Comment = "New comment"
            };

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

            // Mock Expense repository
            var expenseRepositoryMock = new Mock<IExpenseRepository>();

            expenseRepositoryMock.Setup(e => e.GetExpenseById(It.IsAny<int>()))
                .ReturnsAsync(expense);

            var handler = new EditExpenseByIdCommandHandler(expenseRepositoryMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            expenseRepositoryMock.Verify(e => e.Commit(), Times.Once);
            expense.CategoryId.Should().Be(command.CategoryId);
            expense.PaymentId.Should().Be(command.PaymentId);
            expense.Amount.Should().Be(command.Amount);
            expense.ExpenseDate.Should().Be(command.ExpenseDate);
            expense.Comment.Should().Be(command.Comment);
        }
    }
}