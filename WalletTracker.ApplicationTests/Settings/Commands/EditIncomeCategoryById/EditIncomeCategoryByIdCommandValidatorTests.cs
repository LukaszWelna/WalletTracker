using FluentValidation.TestHelper;
using Moq;
using WalletTracker.Application.Settings.Commands.EditExpenseCategoryById;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Settings.Commands.EditIncomeCategoryById.Tests
{
    public class EditIncomeCategoryByIdCommandValidatorTests
    {
        // Prepare valid commands
        public static IEnumerable<object[]> GetSampleValidCommands()
        {
            yield return new object[] {
                new EditIncomeCategoryByIdCommand()
                {
                    Id = 1,
                    Name = "TestName"
                }
            };

            yield return new object[] {
                new EditIncomeCategoryByIdCommand()
                {
                    Id = 200,
                    Name = "TestNameLonger"
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetSampleValidCommands))]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError(EditIncomeCategoryByIdCommand command)
        {
            // Arrange

            // Mock GetByName method
            var incomeCategoryRepositoryMock = new Mock<IIncomeCategoryRepository>();

            incomeCategoryRepositoryMock.Setup(e => e.GetByName(It.IsAny<String>()))
                .ReturnsAsync(null as IncomeCategoryAssignedToUser);

            var validator = new EditIncomeCategoryByIdCommandValidator(incomeCategoryRepositoryMock.Object);

            // Act 
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("")]
        [InlineData("A")]
        [InlineData("TestCategoryNameTooLong12345")]
        public void Validate_WithInvalidCommand_ShouldHaveValidationError(string name)
        {
            // Arrange
            var editIncomeCategoryByIdCommand = new EditIncomeCategoryByIdCommand()
            {
                Name = name
            };

            // Mock GetByName method
            var incomeCategoryRepositoryMock = new Mock<IIncomeCategoryRepository>();

            incomeCategoryRepositoryMock.Setup(e => e.GetByName(It.IsAny<String>()))
                .ReturnsAsync(null as IncomeCategoryAssignedToUser);

            var validator = new EditIncomeCategoryByIdCommandValidator(incomeCategoryRepositoryMock.Object);

            // Act 
            var result = validator.TestValidate(editIncomeCategoryByIdCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Id);
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public void Validate_WithExistingNameAndDifferentIdOfCurrentCategoryAndCategoryFromDb_ShouldHaveValidationError()
        {
            // Arrange
            var editIncomeCategoryByIdCommand = new EditIncomeCategoryByIdCommand()
            {
                Id = 1,
                Name = "TestName"
            };

            // Mock GetByName method
            var incomeCategoryAssignedToUser = new IncomeCategoryAssignedToUser()
            {
                Id = 2,
                Name = "TestName"
            };

            var incomeCategoryRepositoryMock = new Mock<IIncomeCategoryRepository>();

            incomeCategoryRepositoryMock.Setup(e => e.GetByName(It.IsAny<String>()))
                .ReturnsAsync(incomeCategoryAssignedToUser);

            var validator = new EditIncomeCategoryByIdCommandValidator(incomeCategoryRepositoryMock.Object);

            // Act 
            var result = validator.TestValidate(editIncomeCategoryByIdCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }
    }
}