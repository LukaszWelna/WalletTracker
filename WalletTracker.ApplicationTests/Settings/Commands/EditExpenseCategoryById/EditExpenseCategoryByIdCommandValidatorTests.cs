using FluentValidation.TestHelper;
using Moq;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Settings.Commands.EditExpenseCategoryById.Tests
{
    public class EditExpenseCategoryByIdCommandValidatorTests
    {
        // Prepare valid commands
        public static IEnumerable<object[]> GetSampleValidCommands()
        {
            yield return new object[] {
                new EditExpenseCategoryByIdCommand()
                {
                    Id = 1,
                    Name = "TestName",
                    Limit = 10000
                }
            };

            yield return new object[] {
                new EditExpenseCategoryByIdCommand()
                {
                    Id = 200,
                    Name = "TestNameLonger",
                    Limit = 5.50m
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetSampleValidCommands))]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError(EditExpenseCategoryByIdCommand command)
        {
            // Arrange

            // Mock GetByName method
            var expenseCategoryRepositoryMock = new Mock<IExpenseCategoryRepository>();

            expenseCategoryRepositoryMock.Setup(e => e.GetByName(It.IsAny<String>()))
                .ReturnsAsync(null as ExpenseCategoryAssignedToUser);

            var validator = new EditExpenseCategoryByIdCommandValidator(expenseCategoryRepositoryMock.Object);

            // Act 
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        // Prepare invalid commands (invalid id, name length and limit)
        public static IEnumerable<object[]> GetSampleInvalidCommands()
        {
            yield return new object[] {
                new EditExpenseCategoryByIdCommand()
                {
                    Name = "",
                    Limit = 50.555m
                }
            };

            yield return new object[] {
                new EditExpenseCategoryByIdCommand()
                {
                    Name = "TestCategoryNameTooLong12345",
                    Limit = -18
                }
            };

            yield return new object[] {
                new EditExpenseCategoryByIdCommand()
                {
                    Name = "A",
                    Limit = 10000000000
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetSampleInvalidCommands))]
        public void Validate_WithInvalidCommand_ShouldHaveValidationError(EditExpenseCategoryByIdCommand command)
        {
            // Arrange

            // Mock GetByName method
            var expenseCategoryRepositoryMock = new Mock<IExpenseCategoryRepository>();

            expenseCategoryRepositoryMock.Setup(e => e.GetByName(It.IsAny<String>()))
                .ReturnsAsync(null as ExpenseCategoryAssignedToUser);

            var validator = new EditExpenseCategoryByIdCommandValidator(expenseCategoryRepositoryMock.Object);

            // Act 
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Id);
            result.ShouldHaveValidationErrorFor(c => c.Name);
            result.ShouldHaveValidationErrorFor(c => c.Limit);
        }

        [Fact]
        public void Validate_WithExistingNameAndDifferentIdOfCurrentCategoryAndCategoryFromDb_ShouldHaveValidationError()
        {
            // Arrange
            var editExpenseCategoryByIdCommand = new EditExpenseCategoryByIdCommand()
            {
                Id = 1,
                Name = "TestName",
                Limit = 100
            };

            // Mock GetByName method
            var expenseCategoryAssignedToUser = new ExpenseCategoryAssignedToUser()
            {
                Id = 2,
                Name = "TestName",
                Limit = 1000
            };

            var expenseCategoryRepositoryMock = new Mock<IExpenseCategoryRepository>();

            expenseCategoryRepositoryMock.Setup(e => e.GetByName(It.IsAny<String>()))
                .ReturnsAsync(expenseCategoryAssignedToUser);

            var validator = new EditExpenseCategoryByIdCommandValidator(expenseCategoryRepositoryMock.Object);

            // Act 
            var result = validator.TestValidate(editExpenseCategoryByIdCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }
    }
}