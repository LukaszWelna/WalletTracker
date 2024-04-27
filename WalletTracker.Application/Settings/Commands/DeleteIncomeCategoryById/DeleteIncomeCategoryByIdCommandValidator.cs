using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Settings.Commands.DeleteIncomeCategory;

namespace WalletTracker.Application.Settings.Commands.DeleteIncomeCategoryById
{
    public class DeleteIncomeCategoryByIdCommandValidator : AbstractValidator<DeleteIncomeCategoryByIdCommand>
    {
        public DeleteIncomeCategoryByIdCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("This field is required");
        }
    }
}
