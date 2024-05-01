using FluentValidation;

namespace WalletTracker.Application.Settings.Commands.DeletePaymentMethodById
{
    public class DeletePaymentMethodByIdCommandValidator : AbstractValidator<DeletePaymentMethodByIdCommand>
    {
        public DeletePaymentMethodByIdCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("This field is required");
        }
    }
}
