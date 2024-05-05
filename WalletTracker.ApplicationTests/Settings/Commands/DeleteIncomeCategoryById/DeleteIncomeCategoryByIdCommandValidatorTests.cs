using FluentValidation.TestHelper;
using WalletTracker.Application.Settings.Commands.DeleteIncomeCategory;
using Xunit;

namespace WalletTracker.Application.Settings.Commands.DeleteIncomeCategoryById.Tests
{
    public class DeleteIncomeCategoryByIdCommandValidatorTests
    {
        [Fact()]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError()
        {
            // Arrange
            var deleteIncomeCategoryByIdCommand = new DeleteIncomeCategoryByIdCommand()
            {
                Id = 8
            };

            var validator = new DeleteIncomeCategoryByIdCommandValidator();

            // Act
            var result = validator.TestValidate(deleteIncomeCategoryByIdCommand);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validate_WithInvalidCommand_ShouldHaveValidationError()
        {
            // Arrange
            var deleteIncomeCategoryByIdCommand = new DeleteIncomeCategoryByIdCommand();

            var validator = new DeleteIncomeCategoryByIdCommandValidator();

            // Act
            var result = validator.TestValidate(deleteIncomeCategoryByIdCommand);

            // Assert
            result.ShouldHaveAnyValidationError();
        }
    }
}