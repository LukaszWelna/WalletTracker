using AutoMapper;
using MediatR;
using WalletTracker.Application.Expense;
using WalletTracker.Application.Settings.Commands.EditExpenseCategoryById;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Queries.GetExpenseCategoryFormToEdit
{
    public class GetExpenseCategoryFormToEditQueryHandler : IRequestHandler<GetExpenseCategoryFormToEditQuery, EditExpenseCategoryByIdCommand>
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IMapper _mapper;

        public GetExpenseCategoryFormToEditQueryHandler(IExpenseCategoryRepository expenseCategoryRepository, IMapper mapper)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
            _mapper = mapper;
        }

        // Return EditExpenseCategoryByIdCommand object with categories assigned to the logged user
        public async Task<EditExpenseCategoryByIdCommand> Handle(GetExpenseCategoryFormToEditQuery request, CancellationToken cancellationToken)
        {
            var categoriesAssignedToUser = await _expenseCategoryRepository
                .GetCategoriesAssignedToLoggedUser();

            var categoryAssignedToUserDtos = _mapper.Map<List<ExpenseCategoryAssignedToUserDto>>(categoriesAssignedToUser);

            var command = new EditExpenseCategoryByIdCommand()
            {
                UserCategoryDtos = categoryAssignedToUserDtos
            };

            return command;
        }
    }
}
