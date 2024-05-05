using FluentValidation.TestHelper;
using Moq;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Settings.Commands.CreateIncomeCategory.Tests
{
    public class CreateIncomeCategoryCommandValidatorTests
    {
        [Theory()]
        [InlineData("TestName")]
        [InlineData("TestNameLonger12345")]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError(string name)
        {
            // Arrange
            var createIncomeCategoryCommand = new CreateIncomeCategoryCommand()
            {
                Name = name
            };

            // Mock GetByName method
            var incomeCategoryRepositoryMock = new Mock<IIncomeCategoryRepository>();

            incomeCategoryRepositoryMock.Setup(e => e.GetByName(It.IsAny<String>()))
                .ReturnsAsync(null as IncomeCategoryAssignedToUser);

            var validator = new CreateIncomeCategoryCommandValidator(incomeCategoryRepositoryMock.Object);

            // Act
            var result = validator.TestValidate(createIncomeCategoryCommand);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory()]
        [InlineData("")]
        [InlineData("A")]
        [InlineData("TestCategoryNameTooLong12345")]
        public void Validate_WithInvalidLengthOfName_ShouldHaveValidationError(string name)
        {
            // Arrange
            var createIncomeCategoryCommand = new CreateIncomeCategoryCommand()
            {
                Name = name
            };

            // Mock GetByName method
            var incomeCategoryRepositoryMock = new Mock<IIncomeCategoryRepository>();

            incomeCategoryRepositoryMock.Setup(e => e.GetByName(It.IsAny<String>()))
                .ReturnsAsync(null as IncomeCategoryAssignedToUser);

            var validator = new CreateIncomeCategoryCommandValidator(incomeCategoryRepositoryMock.Object);

            // Act
            var result = validator.TestValidate(createIncomeCategoryCommand);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        [Fact()]
        public void Validate_WithExistingName_ShouldHaveValidationError()
        {
            // Arrange
            var createIncomeCategoryCommand = new CreateIncomeCategoryCommand()
            {
                Name = "TestName"
            };

            // Mock GetByName method
            var incomeCategoryAssignedToUser = new IncomeCategoryAssignedToUser()
            {
                Name = "TestName"
            };

            var incomeCategoryRepositoryMock = new Mock<IIncomeCategoryRepository>();

            incomeCategoryRepositoryMock.Setup(e => e.GetByName(It.IsAny<String>()))
                .ReturnsAsync(incomeCategoryAssignedToUser);

            var validator = new CreateIncomeCategoryCommandValidator(incomeCategoryRepositoryMock.Object);

            // Act
            var result = validator.TestValidate(createIncomeCategoryCommand);

            // Assert
            result.ShouldHaveAnyValidationError();
        }
    }
}