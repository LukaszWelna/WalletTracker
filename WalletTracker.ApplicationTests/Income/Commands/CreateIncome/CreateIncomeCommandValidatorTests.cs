﻿using FluentValidation.TestHelper;
using Xunit;

namespace WalletTracker.Application.Income.Commands.CreateIncome.Tests
{
    public class CreateIncomeCommandValidatorTests
    {
        // Prepare valid commands
        public static IEnumerable<object[]> GetSampleValidCommands()
        {
            yield return new object[] {
                new CreateIncomeCommand()
                {
                    Amount = 100,
                    IncomeDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    CategoryId = 1
                }
            };

            yield return new object[] {
                new CreateIncomeCommand()
                {
                    Amount = 20000,
                    IncomeDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-10),
                    CategoryId = 100
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetSampleValidCommands))]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError(CreateIncomeCommand command)
        {
            // Arrange
            var validator = new CreateIncomeCommandValidator();

            // Act 
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        // Prepare invalid commands
        public static IEnumerable<object[]> GetSampleInvalidCommands()
        {
            yield return new object[] {
                new CreateIncomeCommand()
                {
                    Amount = 0,
                    IncomeDate = DateOnly.FromDateTime(DateTime.MinValue),
                    CategoryId = 0,
                    Comment = "Too long text for a comment"
                }
            };

            yield return new object[] {
                new CreateIncomeCommand()
                {
                    Amount = -10,
                    IncomeDate = DateOnly.FromDateTime(DateTime.MinValue),
                    CategoryId = -10,
                    Comment = "Too long text for a comment"
                }
            };

            yield return new object[] {
                new CreateIncomeCommand()
                {
                    Amount = 1000000000,
                    IncomeDate = DateOnly.FromDateTime(new DateTime(1999, 1, 1)),
                    CategoryId = -100,
                    Comment = "Too long text for a comment"
                }
            };

            yield return new object[] {
                new CreateIncomeCommand()
                {
                    Amount = 10.555m,
                    IncomeDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(1),
                    CategoryId = 0,
                    Comment = "Too long text for a comment"
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetSampleInvalidCommands))]
        public void Validate_WithInvalidCommand_ShouldHaveValidationErrors(CreateIncomeCommand command)
        {
            // Arrange
            var validator = new CreateIncomeCommandValidator();

            // Act 
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Amount);
            result.ShouldHaveValidationErrorFor(c => c.IncomeDate);
            result.ShouldHaveValidationErrorFor(c => c.CategoryId);
            result.ShouldHaveValidationErrorFor(c => c.Comment);
        }
    }
}