using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Commands.EditPaymentMethodById
{
    public class EditPaymentMethodByIdCommandHandler : IRequestHandler<EditPaymentMethodByIdCommand>
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public EditPaymentMethodByIdCommandHandler(IPaymentMethodRepository paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        public async Task Handle(EditPaymentMethodByIdCommand request, CancellationToken cancellationToken)
        {
            var paymentMethod = await _paymentMethodRepository.GetById(request.Id);

            paymentMethod.Name = request.Name!;

            await _paymentMethodRepository.Commit();
        }
    }
}
