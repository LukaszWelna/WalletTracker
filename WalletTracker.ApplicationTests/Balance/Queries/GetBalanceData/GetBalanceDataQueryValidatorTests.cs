using FluentValidation.TestHelper;
using Xunit;

namespace WalletTracker.Application.Balance.Queries.GetBalanceData.Tests
{
    public class GetBalanceDataQueryValidatorTests
    {
        // Prepare valid queries
        public static IEnumerable<object[]> GetSampleValidQueries()
        {
            yield return new object[] {
                new GetBalanceDataQuery()
                {
                    StartDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-5),
                    EndDate = DateOnly.FromDateTime(DateTime.UtcNow)
                }
            };

            yield return new object[] {
                new GetBalanceDataQuery()
                {
                    StartDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-50),
                    EndDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-30)
                }
            };

            yield return new object[] {
                new GetBalanceDataQuery()
                {
                    StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    EndDate = DateOnly.FromDateTime(DateTime.UtcNow)
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetSampleValidQueries))]
        public void Validate_WithValidQuery_ShouldNotHaveValidationError(GetBalanceDataQuery query)
        {
            // Arrange
            var validator = new GetBalanceDataQueryValidator();

            // Act 
            var result = validator.TestValidate(query);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        // Prepare invalid queries for both dates
        public static IEnumerable<object[]> GetSampleInvalidQueries()
        {
            yield return new object[] {
                new GetBalanceDataQuery()
                {
                    StartDate = DateOnly.FromDateTime(DateTime.MinValue),
                    EndDate = DateOnly.FromDateTime(DateTime.MinValue)
                }
            };

            yield return new object[] {
                new GetBalanceDataQuery()
                {
                    StartDate = DateOnly.FromDateTime(new DateTime(1999, 1, 1)),
                    EndDate = DateOnly.FromDateTime(new DateTime(1999, 2, 5))
                }
            };

            yield return new object[] {
                new GetBalanceDataQuery()
                {
                    StartDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(1),
                    EndDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(10)
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetSampleInvalidQueries))]
        public void Validate_WithInvalidQuery_ShouldHaveValidationErrors(GetBalanceDataQuery query)
        {
            // Arrange
            var validator = new GetBalanceDataQueryValidator();

            // Act 
            var result = validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(v => v.StartDate);
            result.ShouldHaveValidationErrorFor(v => v.EndDate);
        }

        // Prepare invalid queries for end date (end date must be greater or equal the start date)
        public static IEnumerable<object[]> GetSampleInvalidQueriesForEndDate()
        {
            yield return new object[] {
                new GetBalanceDataQuery()
                {
                    StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    EndDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-10)
                }
            };

            yield return new object[] {
                new GetBalanceDataQuery()
                {
                    StartDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-10),
                    EndDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-11)
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetSampleInvalidQueriesForEndDate))]
        public void Validate_WithInvalidEndDate_ShouldHaveEndDateValidationError(GetBalanceDataQuery query)
        {
            // Arrange
            var validator = new GetBalanceDataQueryValidator();

            // Act 
            var result = validator.TestValidate(query);

            // Assert
            result.ShouldNotHaveValidationErrorFor(q => q.StartDate);
            result.ShouldHaveValidationErrorFor(q => q.EndDate);
        }
    }
}