using FluentAssertions;
using Moq;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Settings.Commands.EditIncomeCategoryById.Tests
{
    public class EditIncomeCategoryByIdCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidCommand_ShouldEditIncomeCategoryById()
        {
            // Arrange
            var command = new EditIncomeCategoryByIdCommand()
            {
                Id = 1,
                Name = "Test category"
            };

            var category = new IncomeCategoryAssignedToUser()
            {
                Id = 1,
                Name = "Modified name"
            };

            // Mock Income category repository
            var incomeCategoryRepositoryMock = new Mock<IIncomeCategoryRepository>();

            incomeCategoryRepositoryMock.Setup(i => i.GetById(It.IsAny<int>()))
                .ReturnsAsync(category);

            var handler = new EditIncomeCategoryByIdCommandHandler(incomeCategoryRepositoryMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            incomeCategoryRepositoryMock.Verify(i => i.Commit(), Times.Once);
            category.Name.Should().Be(command.Name);
        }
    }
}