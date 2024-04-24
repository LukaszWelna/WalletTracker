using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Expense.Commands.EditExpenseById;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Expense.Queries.GetEditExpenseFormDataAfterValidationQuery
{
    public class GetEditExpenseFormDataAfterValidationQueryHandler : IRequestHandler<GetEditExpenseFormDataAfterValidationQuery, EditExpenseByIdCommand>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public GetEditExpenseFormDataAfterValidationQueryHandler(IExpenseRepository expenseRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<EditExpenseByIdCommand> Handle(GetEditExpenseFormDataAfterValidationQuery request, CancellationToken cancellationToken)
        {
            var categoriesAssignedToUser = await _expenseRepository
                .GetCategoriesAssignedToLoggedUser();

            var categoryAssignedToUserDtos = _mapper.Map<List<ExpenseCategoryAssignedToUserDto>>(categoriesAssignedToUser);

            var paymentMethodsAssignedToUser = await _expenseRepository
                .GetPaymentMethodsAssignedToLoggedUser();

            var paymentMethodsAssignedToUserDtos = _mapper.Map<List<PaymentMethodAssignedToUserDto>>(paymentMethodsAssignedToUser);

            request.EditExpenseByIdCommand.UserCategoryDtos = categoryAssignedToUserDtos;
            request.EditExpenseByIdCommand.UserPaymentMethodDtos = paymentMethodsAssignedToUserDtos;

            return request.EditExpenseByIdCommand;
        }
    }
}
