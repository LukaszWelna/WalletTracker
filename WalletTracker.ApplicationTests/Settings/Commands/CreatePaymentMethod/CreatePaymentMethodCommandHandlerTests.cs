using AutoMapper;
using FluentAssertions;
using Moq;
using WalletTracker.Application.ApplicationUser;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Settings.Commands.CreatePaymentMethod.Tests
{
    public class CreatePaymentMethodCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidCommand_ShouldCreatePaymentMethod()
        {
            // Arrange
            var command = new CreatePaymentMethodCommand()
            {
                Id = 1,
                Name = "Test payment method"
            };

            var paymentMethod = new PaymentMethodAssignedToUser()
            {
                Id = 1,
                Name = "Test payment method"
            };

            var user = new CurrentUser("1", "test@test.com");

            // Mock mapper
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<PaymentMethodAssignedToUser>(It.IsAny<CreatePaymentMethodCommand>()))
                .Returns(paymentMethod);

            // Mock User context service
            var userContextServiceMock = new Mock<IUserContextService>();

            userContextServiceMock.Setup(u => u.GetCurrentUser())
                .Returns(user);

            // Mock Payment method repository
            var paymentMethodRepositoryMock = new Mock<IPaymentMethodRepository>();

            var handler = new CreatePaymentMethodCommandHandler(paymentMethodRepositoryMock.Object,
                userContextServiceMock.Object,
                mapperMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            paymentMethodRepositoryMock.Verify(p => p.Create(paymentMethod), Times.Once);
            paymentMethod.UserId.Should().Be(user.Id);
        }
    }
}