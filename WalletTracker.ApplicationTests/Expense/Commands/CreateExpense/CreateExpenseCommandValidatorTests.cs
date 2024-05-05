using FluentValidation.TestHelper;
using WalletTracker.Application.Income.Commands.EditIncomeById;
using Xunit;

namespace WalletTracker.Application.Expense.Commands.CreateExpense.Tests
{
    public class CreateExpenseCommandValidatorTests
    {
        // Prepare valid commands
        public static IEnumerable<object[]> GetSampleValidCommands()
        {
            yield return new object[] {
                new CreateExpenseCommand()
                {
                    Amount = 100,
                    ExpenseDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    PaymentId = 1,
                    CategoryId = 1
                }
            };

            yield return new object[] {
                new CreateExpenseCommand()
                {
                    Amount = 20000,
                    ExpenseDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-10),
                    PaymentId = 25,
                    CategoryId = 100
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetSampleValidCommands))]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError(CreateExpenseCommand command)
        {
            // Arrange
            var validator = new CreateExpenseCommandValidator();

            // Act 
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        // Prepare invalid commands
        public static IEnumerable<object[]> GetSampleInvalidCommands()
        {
            yield return new object[] {
                new CreateExpenseCommand()
                {
                    Amount = 0,
                    ExpenseDate = DateOnly.FromDateTime(DateTime.MinValue),
                    PaymentId = 0,
                    CategoryId = 0
                }
            };

            yield return new object[] {
                new CreateExpenseCommand()
                {
                    Amount = -10,
                    ExpenseDate = DateOnly.FromDateTime(DateTime.MinValue),
                    PaymentId = -15,
                    CategoryId = -10
                }
             };

            yield return new object[] {
                new CreateExpenseCommand()
                {
                    Amount = 1000000000,
                    ExpenseDate = DateOnly.FromDateTime(new DateTime(1999, 1, 1)),
                    PaymentId = -1,
                    CategoryId = -100
                }
            };

            yield return new object[] {
                new CreateExpenseCommand()
                {
                    Amount = 10.555m,
                    ExpenseDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(1),
                    PaymentId = 0,
                    CategoryId = 0
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetSampleInvalidCommands))]
        public void Validate_WithInvalidCommand_ShouldHaveValidationErrors(CreateExpenseCommand command)
        {
            // Arrange
            var validator = new CreateExpenseCommandValidator();

            // Act 
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Amount);
            result.ShouldHaveValidationErrorFor(c => c.ExpenseDate);
            result.ShouldHaveValidationErrorFor(c => c.PaymentId);
            result.ShouldHaveValidationErrorFor(c => c.CategoryId);
        }
    }
}