using FluentValidation.TestHelper;
using Moq;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Settings.Commands.EditPaymentMethodById.Tests
{
    public class EditPaymentMethodByIdCommandValidatorTests
    {
        // Prepare valid commands
        public static IEnumerable<object[]> GetSampleValidCommands()
        {
            yield return new object[] {
                new EditPaymentMethodByIdCommand()
                {
                    Id = 1,
                    Name = "TestName"
                }
            };

            yield return new object[] {
                new EditPaymentMethodByIdCommand()
                {
                    Id = 200,
                    Name = "TestNameLonger"
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetSampleValidCommands))]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError(EditPaymentMethodByIdCommand command)
        {
            // Arrange

            // Mock GetByName method
            var paymentMethodRepositoryMock = new Mock<IPaymentMethodRepository>();

            paymentMethodRepositoryMock.Setup(e => e.GetByName(It.IsAny<String>()))
                .ReturnsAsync(null as PaymentMethodAssignedToUser);

            var validator = new EditPaymentMethodByIdCommandValidator(paymentMethodRepositoryMock.Object);

            // Act 
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("")]
        [InlineData("A")]
        [InlineData("TestPaymentMethodNameTooLong")]
        public void Validate_WithInvalidCommand_ShouldHaveValidationError(string name)
        {
            // Arrange
            var editPaymentMethodByIdCommand = new EditPaymentMethodByIdCommand()
            {
                Name = name
            };

            // Mock GetByName method
            var paymentMethodRepositoryMock = new Mock<IPaymentMethodRepository>();

            paymentMethodRepositoryMock.Setup(e => e.GetByName(It.IsAny<String>()))
                .ReturnsAsync(null as PaymentMethodAssignedToUser);

            var validator = new EditPaymentMethodByIdCommandValidator(paymentMethodRepositoryMock.Object);

            // Act 
            var result = validator.TestValidate(editPaymentMethodByIdCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Id);
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public void Validate_WithExistingNameAndDifferentIdOfCurrentPaymentAndPaymentFromDb_ShouldHaveValidationError()
        {
            // Arrange
            var editPaymentMethodByIdCommand = new EditPaymentMethodByIdCommand()
            {
                Id = 1,
                Name = "TestName"
            };

            // Mock GetByName method
            var paymentMethodAssignedToUser = new PaymentMethodAssignedToUser()
            {
                Id = 2,
                Name = "TestName"
            };

            var paymentMethodRepositoryMock = new Mock<IPaymentMethodRepository>();

            paymentMethodRepositoryMock.Setup(e => e.GetByName(It.IsAny<String>()))
                .ReturnsAsync(paymentMethodAssignedToUser);

            var validator = new EditPaymentMethodByIdCommandValidator(paymentMethodRepositoryMock.Object);

            // Act 
            var result = validator.TestValidate(editPaymentMethodByIdCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }
    }
}