using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Commands.EditIncomeCategoryById
{
    public class EditIncomeCategoryByIdCommandValidator : AbstractValidator<EditIncomeCategoryByIdCommand>
    {
        public EditIncomeCategoryByIdCommandValidator(IIncomeCategoryRepository incomeCategoryRepository)
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("This field is required");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("This field is required.")
                .MinimumLength(2).WithMessage("This field must contain at least 2 characters")
                .MaximumLength(25).WithMessage("This field must contain less than 26 characters")
                .Custom((value, context) =>
                {
                    var command = context.InstanceToValidate as EditIncomeCategoryByIdCommand;

                    if (command != null && !String.IsNullOrEmpty(value))
                    {
                        var existingCategory = incomeCategoryRepository.GetByName(value).Result;

                        if (existingCategory != null && existingCategory.Id != command.Id)
                        {
                            context.AddFailure("Category name already exists in the database.");
                        }
                    }
                });
        }
    }
}
