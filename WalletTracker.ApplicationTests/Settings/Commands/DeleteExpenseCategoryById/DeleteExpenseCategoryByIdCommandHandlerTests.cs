using FluentAssertions;
using Moq;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Settings.Commands.DeleteExpenseCategoryById.Tests
{
    public class DeleteExpenseCategoryByIdCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_WithValidId_DeleteExpenseCategory()
        {
            // Arrange
            var command = new DeleteExpenseCategoryByIdCommand()
            {
                Id = 1
            };

            // Mock Expense category repository
            var expenseCategoryRepositoryMock = new Mock<IExpenseCategoryRepository>();

            var handler = new DeleteExpenseCategoryByIdCommandHandler(expenseCategoryRepositoryMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            expenseCategoryRepositoryMock.Verify(e => e.DeleteById(command.Id), Times.Once);
        }

        [Fact()]
        public void Handle_WithInvalidId_ShouldThrowException()
        {
            // Arrange
            var command = new DeleteExpenseCategoryByIdCommand()
            {
                Id = 0
            };

            // Mock Expense category repository
            var expenseCategoryRepositoryMock = new Mock<IExpenseCategoryRepository>();

            var handler = new DeleteExpenseCategoryByIdCommandHandler(expenseCategoryRepositoryMock.Object);

            // Act
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            func.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}