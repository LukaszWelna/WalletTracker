using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTracker.Application.Settings.Commands.DeleteExpenseCategoryById
{
    public class DeleteExpenseCategoryByIdCommandValidator : AbstractValidator<DeleteExpenseCategoryByIdCommand>
    {
        public DeleteExpenseCategoryByIdCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("This field is required");
        }
    }
}
