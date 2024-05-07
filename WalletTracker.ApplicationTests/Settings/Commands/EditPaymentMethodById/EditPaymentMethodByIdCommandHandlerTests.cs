using FluentAssertions;
using Moq;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Settings.Commands.EditPaymentMethodById.Tests
{
    public class EditPaymentMethodByIdCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidCommand_ShouldEditPaymentMethodById()
        {
            // Arrange
            var command = new EditPaymentMethodByIdCommand()
            {
                Id = 1,
                Name = "Test payment method"
            };

            var paymentMethod = new PaymentMethodAssignedToUser()
            {
                Id = 1,
                Name = "Modified name"
            };

            // Mock Payment method repository
            var paymentMethodRepositoryMock = new Mock<IPaymentMethodRepository>();

            paymentMethodRepositoryMock.Setup(p => p.GetById(It.IsAny<int>()))
                .ReturnsAsync(paymentMethod);

            var handler = new EditPaymentMethodByIdCommandHandler(paymentMethodRepositoryMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            paymentMethodRepositoryMock.Verify(p => p.Commit(), Times.Once);
            paymentMethod.Name.Should().Be(command.Name);
        }
    }
}