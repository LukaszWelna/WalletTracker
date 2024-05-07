using FluentValidation.TestHelper;
using Moq;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Settings.Commands.CreateExpenseCategory.Tests
{
    public class CreateExpenseCategoryCommandValidatorTests
    {
        [Theory]
        [InlineData("TestName")]
        [InlineData("TestNameLonger12345")]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError(string name)
        {
            // Arrange
            var createExpenseCategoryCommand = new CreateExpenseCategoryCommand()
            {
                Name = name
            };

            // Mock GetByName method
            var expenseCategoryRepositoryMock = new Mock<IExpenseCategoryRepository>();

            expenseCategoryRepositoryMock.Setup(e => e.GetByName(It.IsAny<String>()))
                .ReturnsAsync(null as ExpenseCategoryAssignedToUser);

            var validator = new CreateExpenseCategoryCommandValidator(expenseCategoryRepositoryMock.Object);

            // Act
            var result = validator.TestValidate(createExpenseCategoryCommand);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("")]
        [InlineData("A")]
        [InlineData("TestCategoryNameTooLong12345")]
        public void Validate_WithInvalidLengthOfName_ShouldHaveValidationError(string name)
        {
            // Arrange
            var createExpenseCategoryCommand = new CreateExpenseCategoryCommand()
            {
                Name = name
            };

            // Mock GetByName method
            var expenseCategoryRepositoryMock = new Mock<IExpenseCategoryRepository>();

            expenseCategoryRepositoryMock.Setup(e => e.GetByName(It.IsAny<String>()))
                .ReturnsAsync(null as ExpenseCategoryAssignedToUser);

            var validator = new CreateExpenseCategoryCommandValidator(expenseCategoryRepositoryMock.Object);

            // Act
            var result = validator.TestValidate(createExpenseCategoryCommand);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        [Fact]
        public void Validate_WithExistingName_ShouldHaveValidationError()
        {
            // Arrange
            var createExpenseCategoryCommand = new CreateExpenseCategoryCommand()
            {
                Name = "TestName"
            };

            // Mock GetByName method
            var expenseCategoryAssignedToUser = new ExpenseCategoryAssignedToUser()
            {
                Name = "TestName"
            };

            var expenseCategoryRepositoryMock = new Mock<IExpenseCategoryRepository>();

            expenseCategoryRepositoryMock.Setup(e => e.GetByName(It.IsAny<String>()))
                .ReturnsAsync(expenseCategoryAssignedToUser);

            var validator = new CreateExpenseCategoryCommandValidator(expenseCategoryRepositoryMock.Object);

            // Act
            var result = validator.TestValidate(createExpenseCategoryCommand);

            // Assert
            result.ShouldHaveAnyValidationError();
        }
    }
}