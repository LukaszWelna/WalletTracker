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
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IMapper _mapper;

        public GetExpenseFormDataAfterValidationQueryHandler(IExpenseCategoryRepository expenseCategoryRepository, IMapper mapper)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
            _mapper = mapper;
        }

        public async Task<CreateExpenseCommand> Handle(GetExpenseFormDataAfterValidationQuery request, CancellationToken cancellationToken)
        {
            var categoriesAssignedToUser = await _expenseCategoryRepository
                .GetCategoriesAssignedToLoggedUser();

            var categoryAssignedToUserDtos = _mapper.Map<List<ExpenseCategoryAssignedToUserDto>>(categoriesAssignedToUser);

            var paymentMethodsAssignedToUser = await _expenseCategoryRepository
                .GetPaymentMethodsAssignedToLoggedUser();

            var paymentMethodsAssignedToUserDtos = _mapper.Map<List<PaymentMethodAssignedToUserDto>>(paymentMethodsAssignedToUser);

            request.CreateExpenseCommand.UserCategoryDtos = categoryAssignedToUserDtos;
            request.CreateExpenseCommand.UserPaymentMethodDtos = paymentMethodsAssignedToUserDtos;

            return request.CreateExpenseCommand;
        }
    }
}
