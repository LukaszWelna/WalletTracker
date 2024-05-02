using AutoMapper;
using MediatR;
using WalletTracker.Application.Expense.Commands.CreateExpense;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Expense.Queries.GetExpenseFormDataAfterValidation
{
    public class GetExpenseFormDataAfterValidationQueryHandler : IRequestHandler<GetExpenseFormDataAfterValidationQuery, CreateExpenseCommand>
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper _mapper;

        public GetExpenseFormDataAfterValidationQueryHandler(IExpenseCategoryRepository expenseCategoryRepository,
            IPaymentMethodRepository paymentMethodRepository,
            IMapper mapper)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
        }

        // Return CreateExpenseCommand object with categories and payment methods assigned to the logged user
        public async Task<CreateExpenseCommand> Handle(GetExpenseFormDataAfterValidationQuery request, CancellationToken cancellationToken)
        {
            var categoriesAssignedToUser = await _expenseCategoryRepository
                .GetCategoriesAssignedToLoggedUser();

            var categoryAssignedToUserDtos = _mapper.Map<List<ExpenseCategoryAssignedToUserDto>>(categoriesAssignedToUser);

            var paymentMethodsAssignedToUser = await _paymentMethodRepository
                .GetPaymentMethodsAssignedToLoggedUser();

            var paymentMethodsAssignedToUserDtos = _mapper.Map<List<PaymentMethodAssignedToUserDto>>(paymentMethodsAssignedToUser);

            request.CreateExpenseCommand.UserCategoryDtos = categoryAssignedToUserDtos;
            request.CreateExpenseCommand.UserPaymentMethodDtos = paymentMethodsAssignedToUserDtos;

            return request.CreateExpenseCommand;
        }
    }
}
