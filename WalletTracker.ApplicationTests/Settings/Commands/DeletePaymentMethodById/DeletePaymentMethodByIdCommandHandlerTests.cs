using FluentAssertions;
using Moq;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Settings.Commands.DeletePaymentMethodById.Tests
{
    public class DeletePaymentMethodByIdCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidId_ShouldDeletePaymentMethod()
        {
            // Arrange
            var command = new DeletePaymentMethodByIdCommand()
            {
                Id = 1
            };

            // Mock Payment method repository
            var paymentMethodRepositoryMock = new Mock<IPaymentMethodRepository>();

            var handler = new DeletePaymentMethodByIdCommandHandler(paymentMethodRepositoryMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            paymentMethodRepositoryMock.Verify(p => p.DeleteById(command.Id), Times.Once);
        }

        [Fact]
        public void Handle_WithInvalidId_ShouldThrowException()
        {
            // Arrange
            var command = new DeletePaymentMethodByIdCommand()
            {
                Id = 0
            };

            // Mock Payment method repository
            var paymentMethodRepositoryMock = new Mock<IPaymentMethodRepository>();

            var handler = new DeletePaymentMethodByIdCommandHandler(paymentMethodRepositoryMock.Object);

            // Act
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            func.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}