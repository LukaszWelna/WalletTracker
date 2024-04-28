using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Expense;
using WalletTracker.Application.Expense.Commands.DeleteExpenseById;
using WalletTracker.Application.Income;
using WalletTracker.Application.Settings.Commands.DeleteExpenseCategoryById;
using WalletTracker.Application.Settings.Commands.DeleteIncomeCategory;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Queries.GetExpenseCategoryFormToDelete
{
    public class GetExpenseCategoryFormToDeleteQueryHandler : IRequestHandler<GetExpenseCategoryFormToDeleteQuery, DeleteExpenseCategoryByIdCommand>
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IMapper _mapper;

        public GetExpenseCategoryFormToDeleteQueryHandler(IExpenseCategoryRepository expenseCategoryRepository, IMapper mapper)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
            _mapper = mapper;
        }

        public async Task<DeleteExpenseCategoryByIdCommand> Handle(GetExpenseCategoryFormToDeleteQuery request, CancellationToken cancellationToken)
        {
            var categoriesAssignedToUser = await _expenseCategoryRepository
                .GetCategoriesAssignedToLoggedUser();

            var categoryAssignedToUserDtos = _mapper.Map<List<ExpenseCategoryAssignedToUserDto>>(categoriesAssignedToUser);

            var command = new DeleteExpenseCategoryByIdCommand()
            {
                UserCategoryDtos = categoryAssignedToUserDtos
            };

            return command;
        }
    }
}
