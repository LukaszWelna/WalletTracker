using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Commands.CreatePaymentMethod
{
    public class CreatePaymentMethodCommandValidator : AbstractValidator<CreatePaymentMethodCommand>
    {
        public CreatePaymentMethodCommandValidator(IPaymentMethodRepository paymentMethodRepository)
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("This field is required.")
                .MinimumLength(2).WithMessage("This field must contain at least 2 characters")
                .MaximumLength(25).WithMessage("This field must contain less than 26 characters")
                .Custom((value, context) =>
                {
                    if (!String.IsNullOrEmpty(value))
                    {
                        var existingCategory = paymentMethodRepository.GetByName(value).Result;

                        if (existingCategory != null)
                        {
                            context.AddFailure("Payment method name already exists in the database.");
                        }
                    }
                });
        }
    }
}
