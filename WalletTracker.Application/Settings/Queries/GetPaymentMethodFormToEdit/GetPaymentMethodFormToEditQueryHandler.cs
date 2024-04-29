using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Expense;
using WalletTracker.Application.Income;
using WalletTracker.Application.Settings.Commands.EditIncomeCategoryById;
using WalletTracker.Application.Settings.Commands.EditPaymentMethodById;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Queries.GetPaymentMethodFormToEdit
{
    public class GetPaymentMethodFormToEditQueryHandler : IRequestHandler<GetPaymentMethodFormToEditQuery, EditPaymentMethodByIdCommand>
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper _mapper;

        public GetPaymentMethodFormToEditQueryHandler(IPaymentMethodRepository paymentMethodRepository, IMapper mapper)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
        }

        public async Task<EditPaymentMethodByIdCommand> Handle(GetPaymentMethodFormToEditQuery request, CancellationToken cancellationToken)
        {
            var paymentMethodsAssignedToUser = await _paymentMethodRepository
                .GetPaymentMethodsAssignedToLoggedUser();

            var paymentMethodAssignedToUserDtos = _mapper.Map<List<PaymentMethodAssignedToUserDto>>(paymentMethodsAssignedToUser);

            var command = new EditPaymentMethodByIdCommand()
            {
                UserPaymentMethodDtos = paymentMethodAssignedToUserDtos
            };

            return command;
        }
    }
}
