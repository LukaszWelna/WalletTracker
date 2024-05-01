using FluentValidation;

namespace WalletTracker.Application.Balance.Queries.GetBalanceData
{
    public class GetBalanceDataQueryValidator : AbstractValidator<GetBalanceDataQuery>
    {
        public GetBalanceDataQueryValidator()
        {
            RuleFor(b => b.StartDate)
                .NotEmpty().WithMessage("This field is required.")
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(new DateTime(2000, 1, 1))).WithMessage("Please enter a date equal or greater than 01-01-2000.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("Date must be equal or earlier than current date.");

            RuleFor(b => b.EndDate)
                .NotEmpty().WithMessage("This field is required.")
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(new DateTime(2000, 1, 1))).WithMessage("Please enter a date equal or greater than 01-01-2000.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("Date must be equal or earlier than current date.")
                .GreaterThanOrEqualTo(b => b.StartDate).WithMessage("End date must be equal or greater than start date.");
        }
    }
}
