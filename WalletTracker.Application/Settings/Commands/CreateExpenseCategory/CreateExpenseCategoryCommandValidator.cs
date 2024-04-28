using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Commands.CreateExpenseCategory
{
    public class CreateExpenseCategoryCommandValidator : AbstractValidator<CreateExpenseCategoryCommand>
    {
        public CreateExpenseCategoryCommandValidator(IExpenseCategoryRepository expenseCategoryRepository)
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("This field is required.")
                .MinimumLength(2).WithMessage("This field must contain at least 2 characters")
                .MaximumLength(25).WithMessage("This field must contain less than 26 characters")
                .Custom((value, context) =>
                {
                    if (!String.IsNullOrEmpty(value))
                    {
                        var existingCategory = expenseCategoryRepository.GetByName(value).Result;

                        if (existingCategory != null)
                        {
                            context.AddFailure("Category name already exists in the database.");
                        }
                    }
                });
        }
    }
}
