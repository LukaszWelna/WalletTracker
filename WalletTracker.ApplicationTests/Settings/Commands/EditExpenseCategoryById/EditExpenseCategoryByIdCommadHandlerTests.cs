using FluentAssertions;
using Moq;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Settings.Commands.EditExpenseCategoryById.Tests
{
    public class EditExpenseCategoryByIdCommadHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidCommand_ShouldEditExpenseCategoryById()
        {
            // Arrange
            var command = new EditExpenseCategoryByIdCommand()
            {
                Id = 1,
                Name = "Test category",
                Limit = 1000,
                LimitIsActive = true
            };

            var category = new ExpenseCategoryAssignedToUser()
            {
                Id = 1,
                Name = "Modified name",
                Limit = 5000,
                LimitIsActive = true
            };

            // Mock Expense category repository
            var expenseCategoryRepositoryMock = new Mock<IExpenseCategoryRepository>();

            expenseCategoryRepositoryMock.Setup(e => e.GetById(It.IsAny<int>()))
                .ReturnsAsync(category);

            var handler = new EditExpenseCategoryByIdCommadHandler(expenseCategoryRepositoryMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            expenseCategoryRepositoryMock.Verify(e => e.Commit(), Times.Once);
            category.Name.Should().Be(command.Name);
            category.Limit.Should().Be(command.Limit);
            category.LimitIsActive.Should().Be(command.LimitIsActive);
        }
    }
}