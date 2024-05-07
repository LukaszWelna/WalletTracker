using FluentAssertions;
using Moq;
using WalletTracker.Application.Settings.Commands.DeleteIncomeCategory;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Settings.Commands.DeleteIncomeCategoryById.Tests
{
    public class DeleteIncomeCategoryByIdCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidId_ShouldDeleteIncomeCategory()
        {
            // Arrange
            var command = new DeleteIncomeCategoryByIdCommand()
            {
                Id = 1
            };

            // Mock Income category repository
            var incomeCategoryRepositoryMock = new Mock<IIncomeCategoryRepository>();

            var handler = new DeleteIncomeCategoryByIdCommandHandler(incomeCategoryRepositoryMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            incomeCategoryRepositoryMock.Verify(i => i.DeleteById(command.Id), Times.Once);
        }

        [Fact]
        public void Handle_WithInvalidId_ShouldThrowException()
        {
            // Arrange
            var command = new DeleteIncomeCategoryByIdCommand()
            {
                Id = 0
            };

            // Mock Income category repository
            var incomeCategoryRepositoryMock = new Mock<IIncomeCategoryRepository>();

            var handler = new DeleteIncomeCategoryByIdCommandHandler(incomeCategoryRepositoryMock.Object);

            // Act
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            func.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}