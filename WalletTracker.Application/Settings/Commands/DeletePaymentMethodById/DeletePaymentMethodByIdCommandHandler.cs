using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Commands.DeletePaymentMethodById
{
    public class DeletePaymentMethodByIdCommandHandler : IRequestHandler<DeletePaymentMethodByIdCommand>
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public DeletePaymentMethodByIdCommandHandler(IPaymentMethodRepository paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        public async Task Handle(DeletePaymentMethodByIdCommand request, CancellationToken cancellationToken)
        {
            await _paymentMethodRepository.DeleteById(request.Id);
        }
    }
}
