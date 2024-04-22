using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Expense.Commands.CreateExpense;
using WalletTracker.Application.Income;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Expense.Queries.GetExpenseFormDataAfterValidation
{
    public class GetExpenseFormDataAfterValidationQueryHandler : IRequestHandler<GetExpenseFormDataAfterValidationQuery, CreateExpenseCommand>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public GetExpenseFormDataAfterValidationQueryHandler(IExpenseRepository expenseRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<CreateExpenseCommand> Handle(GetExpenseFormDataAfterValidationQuery request, CancellationToken cancellationToken)
        {
            var categoriesAssignedToUser = await _expenseRepository
                .GetCategoriesAssignedToLoggedUser();

            var categoryAssignedToUserDtos = _mapper.Map<List<ExpenseCategoryAssignedToUserDto>>(categoriesAssignedToUser);

            var paymentMethodsAssignedToUser = await _expenseRepository
                .GetPaymentMethodsAssignedToLoggedUser();

            var paymentMethodsAssignedToUserDtos = _mapper.Map<List<PaymentMethodAssignedToUserDto>>(paymentMethodsAssignedToUser);

            request.CreateExpenseCommand.UserCategoryDtos = categoryAssignedToUserDtos;
            request.CreateExpenseCommand.UserPaymentMethodDtos = paymentMethodsAssignedToUserDtos;

            return request.CreateExpenseCommand;
        }
    }
}
