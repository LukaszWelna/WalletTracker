using AutoMapper;
using MediatR;
using WalletTracker.Application.ApplicationUser;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Commands.CreatePaymentMethod
{
    public class CreatePaymentMethodCommandHandler : IRequestHandler<CreatePaymentMethodCommand>
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public CreatePaymentMethodCommandHandler(IPaymentMethodRepository paymentMethodRepository,
            IUserContextService userContextService,
            IMapper mapper)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task Handle(CreatePaymentMethodCommand request, CancellationToken cancellationToken)
        {
            var paymentMethod = _mapper.Map<PaymentMethodAssignedToUser>(request);

            paymentMethod.UserId = _userContextService.GetCurrentUser().Id;

            await _paymentMethodRepository.Create(paymentMethod);
        }
    }
}
