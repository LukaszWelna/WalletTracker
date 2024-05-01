using AutoMapper;
using MediatR;
using WalletTracker.Application.Expense.Commands.EditExpenseById;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Expense.Queries.EditExpenseById
{
    public class EditExpenseByIdQueryHandler : IRequestHandler<EditExpenseByIdQuery, EditExpenseByIdCommand>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper _mapper;

        public EditExpenseByIdQueryHandler(IExpenseRepository expenseRepository,
            IExpenseCategoryRepository expenseCategoryRepository,
            IPaymentMethodRepository paymentMethodRepository,
            IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _expenseCategoryRepository = expenseCategoryRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
        }

        public async Task<EditExpenseByIdCommand> Handle(EditExpenseByIdQuery request, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository.GetExpenseById(request.ExpenseId);

            var command = _mapper.Map<EditExpenseByIdCommand>(expense);

            var categoriesAssignedToUser = await _expenseCategoryRepository
                .GetCategoriesAssignedToLoggedUser();

            var categoryAssignedToUserDtos = _mapper.Map<List<ExpenseCategoryAssignedToUserDto>>(categoriesAssignedToUser);

            var paymentMethodsAssignedToUser = await _paymentMethodRepository
                .GetPaymentMethodsAssignedToLoggedUser();

            var paymentMethodsAssignedToUserDtos = _mapper.Map<List<PaymentMethodAssignedToUserDto>>(paymentMethodsAssignedToUser);

            command.UserCategoryDtos = categoryAssignedToUserDtos;
            command.UserPaymentMethodDtos = paymentMethodsAssignedToUserDtos;

            return command;
        }
    }
}
