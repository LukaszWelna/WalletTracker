using FluentAssertions;
using Moq;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Expense.Commands.DeleteExpenseById.Tests
{
    public class DeleteExpenseByIdCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_WithValidId_DeleteExpense()
        {
            // Arrange
            var command = new DeleteExpenseByIdCommand(1);

            // Mock Expense repository
            var expenseRepositoryMock = new Mock<IExpenseRepository>();

            var handler = new DeleteExpenseByIdCommandHandler(expenseRepositoryMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            expenseRepositoryMock.Verify(e => e.DeleteExpenseById(command.ExpenseId), Times.Once);
        }

        [Fact()]
        public void Handle_WithInvalidId_ShouldThrowException()
        {
            // Arrange
            var command = new DeleteExpenseByIdCommand(0);

            // Mock Expense repository
            var expenseRepositoryMock = new Mock<IExpenseRepository>();

            var handler = new DeleteExpenseByIdCommandHandler(expenseRepositoryMock.Object);

            // Act
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            func.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}