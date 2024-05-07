using FluentValidation.TestHelper;
using Moq;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Settings.Commands.CreatePaymentMethod.Tests
{
    public class CreatePaymentMethodCommandValidatorTests
    {
        [Theory]
        [InlineData("TestName")]
        [InlineData("TestNameLonger12345")]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError(string name)
        {
            // Arrange
            var createPaymentMethodCommand = new CreatePaymentMethodCommand()
            {
                Name = name
            };

            // Mock GetByName method
            var paymentMethodRepositoryMock = new Mock<IPaymentMethodRepository>();

            paymentMethodRepositoryMock.Setup(p => p.GetByName(It.IsAny<String>()))
                .ReturnsAsync(null as PaymentMethodAssignedToUser);

            var validator = new CreatePaymentMethodCommandValidator(paymentMethodRepositoryMock.Object);

            // Act
            var result = validator.TestValidate(createPaymentMethodCommand);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("")]
        [InlineData("A")]
        [InlineData("TestPaymentMethodNameTooLong")]
        public void Validate_WithInvalidLengthOfName_ShouldHaveValidationError(string name)
        {
            // Arrange
            var createPaymentMethodCommand = new CreatePaymentMethodCommand()
            {
                Name = name
            };

            // Mock GetByName method
            var paymentMethodRepositoryMock = new Mock<IPaymentMethodRepository>();

            paymentMethodRepositoryMock.Setup(p => p.GetByName(It.IsAny<String>()))
                .ReturnsAsync(null as PaymentMethodAssignedToUser);

            var validator = new CreatePaymentMethodCommandValidator(paymentMethodRepositoryMock.Object);

            // Act
            var result = validator.TestValidate(createPaymentMethodCommand);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        [Fact]
        public void Validate_WithExistingName_ShouldHaveValidationError()
        {
            // Arrange
            var createPaymentMethodCommand = new CreatePaymentMethodCommand()
            {
                Name = "TestName"
            };

            // Mock GetByName method
            var paymentMethodAssignedToUser = new PaymentMethodAssignedToUser()
            {
                Name = "TestName"
            };

            var paymentMethodRepositoryMock = new Mock<IPaymentMethodRepository>();

            paymentMethodRepositoryMock.Setup(p => p.GetByName(It.IsAny<String>()))
                .ReturnsAsync(paymentMethodAssignedToUser);

            var validator = new CreatePaymentMethodCommandValidator(paymentMethodRepositoryMock.Object);

            // Act
            var result = validator.TestValidate(createPaymentMethodCommand);

            // Assert
            result.ShouldHaveAnyValidationError();
        }
    }
}