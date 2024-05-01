using FluentValidation;

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
