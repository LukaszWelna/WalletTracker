using MediatR;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Queries.GetExpenseCategoryByName
{
    public class GetExpenseCategoryByNameQueryHandler : IRequestHandler<GetExpenseCategoryByNameQuery, ExpenseCategoryAssignedToUser?>
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;

        public GetExpenseCategoryByNameQueryHandler(IExpenseCategoryRepository expenseCategoryRepository)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
        }

        public async Task<ExpenseCategoryAssignedToUser?> Handle(GetExpenseCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            var category = await _expenseCategoryRepository.GetByName(request.Name);

            return category;
        }
    }
}
