using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Settings.Commands.EditIncomeCategoryById;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Commands.EditExpenseCategoryById
{
    public class EditExpenseCategoryByIdCommandValidator: AbstractValidator<EditExpenseCategoryByIdCommand>
    {
        public EditExpenseCategoryByIdCommandValidator(IExpenseCategoryRepository expenseCategoryRepository)
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("This field is required");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("This field is required.")
                .MinimumLength(2).WithMessage("This field must contain at least 2 characters")
                .MaximumLength(25).WithMessage("This field must contain less than 26 characters")
                .Custom((value, context) =>
                {
                    var command = context.InstanceToValidate as EditExpenseCategoryByIdCommand;

                    if (command != null && !String.IsNullOrEmpty(value))
                    {
                        var existingCategory = expenseCategoryRepository.GetByName(value).Result;

                        if (existingCategory != null && existingCategory.Id != command.Id)
                        {
                            context.AddFailure("Category name already exists in the database.");
                        }
                    }
                });

            RuleFor(i => i.Limit)
                .GreaterThanOrEqualTo(0).WithMessage("Limit must be greater than or equal to 0.")
                .LessThan(100000000).WithMessage("Please enter a value lower than 100000000.")
                .PrecisionScale(10, 2, true).WithMessage("Limit must contain max 2 digits after decimal point.");
        }
    }
}
