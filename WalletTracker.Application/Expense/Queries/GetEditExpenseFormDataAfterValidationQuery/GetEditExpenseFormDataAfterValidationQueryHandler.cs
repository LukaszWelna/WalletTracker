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
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IMapper _mapper;

        public GetEditExpenseFormDataAfterValidationQueryHandler(IExpenseCategoryRepository expenseCategoryRepository, IMapper mapper)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
            _mapper = mapper;
        }

        public async Task<EditExpenseByIdCommand> Handle(GetEditExpenseFormDataAfterValidationQuery request, CancellationToken cancellationToken)
        {
            var categoriesAssignedToUser = await _expenseCategoryRepository
                .GetCategoriesAssignedToLoggedUser();

            var categoryAssignedToUserDtos = _mapper.Map<List<ExpenseCategoryAssignedToUserDto>>(categoriesAssignedToUser);

            var paymentMethodsAssignedToUser = await _expenseCategoryRepository
                .GetPaymentMethodsAssignedToLoggedUser();

            var paymentMethodsAssignedToUserDtos = _mapper.Map<List<PaymentMethodAssignedToUserDto>>(paymentMethodsAssignedToUser);

            request.EditExpenseByIdCommand.UserCategoryDtos = categoryAssignedToUserDtos;
            request.EditExpenseByIdCommand.UserPaymentMethodDtos = paymentMethodsAssignedToUserDtos;

            return request.EditExpenseByIdCommand;
        }
    }
}
