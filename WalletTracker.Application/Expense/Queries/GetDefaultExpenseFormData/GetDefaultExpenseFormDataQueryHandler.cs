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
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper _mapper;

        public GetDefaultExpenseFormDataQueryHandler(IExpenseCategoryRepository expenseCategoryRepository, 
            IPaymentMethodRepository paymentMethodRepository,
            IMapper mapper)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
        }

        public async Task<CreateExpenseCommand> Handle(GetDefaultExpenseFormDataQuery request, CancellationToken cancellationToken)
        {
            var categoriesAssignedToUser = await _expenseCategoryRepository
                .GetCategoriesAssignedToLoggedUser();

            var categoryAssignedToUserDtos = _mapper.Map<List<ExpenseCategoryAssignedToUserDto>>(categoriesAssignedToUser);

            var paymentMethodsAssignedToUser = await _paymentMethodRepository
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
