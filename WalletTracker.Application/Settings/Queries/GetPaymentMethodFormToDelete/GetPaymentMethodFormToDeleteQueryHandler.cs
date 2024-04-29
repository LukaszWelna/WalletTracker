using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Expense;
using WalletTracker.Application.Settings.Commands.DeleteExpenseCategoryById;
using WalletTracker.Application.Settings.Commands.DeletePaymentMethodById;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Queries.GetPaymentMethodFormToDelete
{
    public class GetPaymentMethodFormToDeleteQueryHandler : IRequestHandler<GetPaymentMethodFormToDeleteQuery, DeletePaymentMethodByIdCommand>
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper _mapper;

        public GetPaymentMethodFormToDeleteQueryHandler(IPaymentMethodRepository paymentMethodRepository, IMapper mapper)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
        }

        public async Task<DeletePaymentMethodByIdCommand> Handle(GetPaymentMethodFormToDeleteQuery request, CancellationToken cancellationToken)
        {
            var paymentMethodsAssignedToUser = await _paymentMethodRepository
                .GetPaymentMethodsAssignedToLoggedUser();

            var paymentMethodAssignedToUserDtos = _mapper.Map<List<PaymentMethodAssignedToUserDto>>(paymentMethodsAssignedToUser);

            var command = new DeletePaymentMethodByIdCommand()
            {
                UserPaymentMethodDtos = paymentMethodAssignedToUserDtos
            };

            return command;
        }
    }
}
