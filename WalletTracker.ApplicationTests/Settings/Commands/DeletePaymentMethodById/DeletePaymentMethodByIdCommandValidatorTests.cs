using FluentValidation.TestHelper;
using Xunit;

namespace WalletTracker.Application.Settings.Commands.DeletePaymentMethodById.Tests
{
    public class DeletePaymentMethodByIdCommandValidatorTests
    {
        [Fact()]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError()
        {
            // Arrange
            var deletePaymentMethodByIdCommand = new DeletePaymentMethodByIdCommand()
            {
                Id = 8
            };

            var validator = new DeletePaymentMethodByIdCommandValidator();

            // Act
            var result = validator.TestValidate(deletePaymentMethodByIdCommand);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validate_WithInvalidCommand_ShouldHaveValidationError()
        {
            // Arrange
            var deletePaymentMethodByIdCommand = new DeletePaymentMethodByIdCommand();

            var validator = new DeletePaymentMethodByIdCommandValidator();

            // Act
            var result = validator.TestValidate(deletePaymentMethodByIdCommand);

            // Assert
            result.ShouldHaveAnyValidationError();
        }
    }
}