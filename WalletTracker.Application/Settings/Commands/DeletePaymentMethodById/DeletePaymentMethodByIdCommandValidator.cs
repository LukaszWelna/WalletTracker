using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
