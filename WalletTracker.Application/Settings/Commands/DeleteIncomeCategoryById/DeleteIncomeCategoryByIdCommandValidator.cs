using FluentValidation;
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
