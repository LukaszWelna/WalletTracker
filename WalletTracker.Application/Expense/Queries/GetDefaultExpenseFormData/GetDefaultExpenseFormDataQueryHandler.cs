using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Expense.Commands.CreateExpense;
using WalletTracker.Application.Income.Commands.CreateIncome;
using WalletTracker.Application.Income;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Expense.Queries.GetDefaultExpenseFormData
{
    public class GetDefaultExpenseFormDataQueryHandler : IRequestHandler<GetDefaultExpenseFormDataQuery, CreateExpenseCommand>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public GetDefaultExpenseFormDataQueryHandler(IExpenseRepository expenseRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<CreateExpenseCommand> Handle(GetDefaultExpenseFormDataQuery request, CancellationToken cancellationToken)
        {
            var categoriesAssignedToUser = await _expenseRepository
                .GetCategoriesAssignedToLoggedUser();

            var categoryAssignedToUserDtos = _mapper.Map<List<ExpenseCategoryAssignedToUserDto>>(categoriesAssignedToUser);

            var paymentMethodsAssignedToUser = await _expenseRepository
                .GetPaymentMethodsAssignedToLoggedUser();

            var paymentMethodsAssignedToUserDtos = _mapper.Map<List<PaymentMethodAssignedToUserDto>>(paymentMethodsAssignedToUser);

            var command = new CreateExpenseCommand()
            {
                ExpenseDate = DateOnly.FromDateTime(DateTime.UtcNow),
                UserCategoryDtos = categoryAssignedToUserDtos,
                UserPaymentMethodDtos = paymentMethodsAssignedToUserDtos
            };

            return command;
        }
    }
}
