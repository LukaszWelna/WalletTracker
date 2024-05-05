using FluentValidation.TestHelper;
using Xunit;

namespace WalletTracker.Application.Settings.Commands.DeleteExpenseCategoryById.Tests
{
    public class DeleteExpenseCategoryByIdCommandValidatorTests
    {
        [Fact()]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError()
        {
            // Arrange
            var deleteExpenseCategoryByIdCommand = new DeleteExpenseCategoryByIdCommand()
            {
                Id = 8
            };

            var validator = new DeleteExpenseCategoryByIdCommandValidator();

            // Act
            var result = validator.TestValidate(deleteExpenseCategoryByIdCommand);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validate_WithInvalidCommand_ShouldHaveValidationError()
        {
            // Arrange
            var deleteExpenseCategoryByIdCommand = new DeleteExpenseCategoryByIdCommand();

            var validator = new DeleteExpenseCategoryByIdCommandValidator();

            // Act
            var result = validator.TestValidate(deleteExpenseCategoryByIdCommand);

            // Assert
            result.ShouldHaveAnyValidationError();
        }
    }
}