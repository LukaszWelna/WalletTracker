using MediatR;
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

            // Edit current data by value specified in the view
            paymentMethod.Name = request.Name!;

            await _paymentMethodRepository.Commit();
        }
    }
}
