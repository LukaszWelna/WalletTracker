using FluentValidation;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Commands.EditPaymentMethodById
{
    public class EditPaymentMethodByIdCommandValidator : AbstractValidator<EditPaymentMethodByIdCommand>
    {
        public EditPaymentMethodByIdCommandValidator(IPaymentMethodRepository paymentMethodRepository)
        {
            RuleFor(c => c.Id)
                    .NotEmpty().WithMessage("This field is required");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("This field is required.")
                .MinimumLength(2).WithMessage("This field must contain at least 2 characters")
                .MaximumLength(25).WithMessage("This field must contain less than 26 characters")
                .Custom((value, context) =>
                {
                    var command = context.InstanceToValidate as EditPaymentMethodByIdCommand;

                    if (command != null && !String.IsNullOrEmpty(value))
                    {
                        var existingPaymentMethod = paymentMethodRepository.GetByName(value).Result;

                        if (existingPaymentMethod != null && existingPaymentMethod.Id != command.Id)
                        {
                            context.AddFailure("Payment method name already exists in the database.");
                        }
                    }
                });
        }
    }
}
