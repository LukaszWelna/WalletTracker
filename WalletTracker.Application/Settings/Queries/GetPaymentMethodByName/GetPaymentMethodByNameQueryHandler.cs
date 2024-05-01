using MediatR;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Queries.GetPaymentMethodByName
{
    public class GetPaymentMethodByNameQueryHandler : IRequestHandler<GetPaymentMethodByNameQuery, PaymentMethodAssignedToUser?>
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public GetPaymentMethodByNameQueryHandler(IPaymentMethodRepository paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        public async Task<PaymentMethodAssignedToUser?> Handle(GetPaymentMethodByNameQuery request, CancellationToken cancellationToken)
        {
            var paymentMethod = await _paymentMethodRepository.GetByName(request.Name);

            return paymentMethod;
        }
    }
}
