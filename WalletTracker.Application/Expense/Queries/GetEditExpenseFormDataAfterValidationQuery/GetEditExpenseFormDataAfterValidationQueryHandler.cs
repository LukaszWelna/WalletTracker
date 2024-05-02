using AutoMapper;
using MediatR;
using WalletTracker.Application.Expense.Commands.EditExpenseById;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Expense.Queries.GetEditExpenseFormDataAfterValidationQuery
{
    public class GetEditExpenseFormDataAfterValidationQueryHandler : IRequestHandler<GetEditExpenseFormDataAfterValidationQuery, EditExpenseByIdCommand>
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper _mapper;

        public GetEditExpenseFormDataAfterValidationQueryHandler(IExpenseCategoryRepository expenseCategoryRepository,
            IPaymentMethodRepository paymentMethodRepository,
            IMapper mapper)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
        }

        // Return EditExpenseByIdCommand object with categories and payment methods assigned to the logged user
        public async Task<EditExpenseByIdCommand> Handle(GetEditExpenseFormDataAfterValidationQuery request, CancellationToken cancellationToken)
        {
            var categoriesAssignedToUser = await _expenseCategoryRepository
                .GetCategoriesAssignedToLoggedUser();

            var categoryAssignedToUserDtos = _mapper.Map<List<ExpenseCategoryAssignedToUserDto>>(categoriesAssignedToUser);

            var paymentMethodsAssignedToUser = await _paymentMethodRepository
                .GetPaymentMethodsAssignedToLoggedUser();

            var paymentMethodsAssignedToUserDtos = _mapper.Map<List<PaymentMethodAssignedToUserDto>>(paymentMethodsAssignedToUser);

            request.EditExpenseByIdCommand.UserCategoryDtos = categoryAssignedToUserDtos;
            request.EditExpenseByIdCommand.UserPaymentMethodDtos = paymentMethodsAssignedToUserDtos;

            return request.EditExpenseByIdCommand;
        }
    }
}
