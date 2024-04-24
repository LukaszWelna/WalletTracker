using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTracker.Application.Income.Commands.EditIncomeById
{
    public class EditIncomeByIdValidator : AbstractValidator<EditIncomeByIdCommand>
    {
        public EditIncomeByIdValidator()
        {
            RuleFor(i => i.Amount)
                .NotEmpty().WithMessage("This field is required.")
                .GreaterThan(0).WithMessage("This field is required.")
                .LessThan(100000000).WithMessage("Please enter a value lower than 100000000.")
                .PrecisionScale(10, 2, true).WithMessage("Amount must contain max 2 digits after decimal point.");

            RuleFor(i => i.IncomeDate)
                .NotEmpty().WithMessage("This field is required.")
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(new DateTime(2000, 1, 1))).WithMessage("Please enter a date equal or greather than 01-01-2000.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("Date must be equal or earlier than current date.");

            RuleFor(i => i.CategoryId)
                .NotEmpty().WithMessage("This field is required.")
                .GreaterThan(0).WithMessage("This field is required");
        }
    }
}
