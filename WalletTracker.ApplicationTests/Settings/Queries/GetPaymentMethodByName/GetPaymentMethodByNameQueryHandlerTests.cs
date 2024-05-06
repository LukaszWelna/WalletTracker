using FluentAssertions;
using Moq;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Settings.Queries.GetPaymentMethodByName.Tests
{
    public class GetPaymentMethodByNameQueryHandlerTests
    {
        [Fact()]
        public async Task Handle_ForGivenName_ReturnPaymentMethodAssignedToUser()
        {
            // Arrange 
            var query = new GetPaymentMethodByNameQuery("Test name");

            var paymentMethod = new PaymentMethodAssignedToUser()
            {
                Id = 1,
                Name = "Test payment method"
            };

            // Mock Payment method repository
            var paymentMethodRepositoryMock = new Mock<IPaymentMethodRepository>();

            paymentMethodRepositoryMock.Setup(p => p.GetByName(It.IsAny<string>()))
                .ReturnsAsync(paymentMethod);

            var handler = new GetPaymentMethodByNameQueryHandler(paymentMethodRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(paymentMethod);
        }
    }
}