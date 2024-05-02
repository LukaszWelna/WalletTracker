using MediatR;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Commands.EditExpenseCategoryById
{
    public class EditExpenseCategoryByIdCommadHandler : IRequestHandler<EditExpenseCategoryByIdCommand>
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;

        public EditExpenseCategoryByIdCommadHandler(IExpenseCategoryRepository expenseCategoryRepository)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
        }

        public async Task Handle(EditExpenseCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            var expenseCategory = await _expenseCategoryRepository.GetById(request.Id);

            // Edit current data by values specified in the view
            expenseCategory.Name = request.Name!;
            expenseCategory.Limit = request.Limit;
            expenseCategory.LimitIsActive = request.LimitIsActive;

            await _expenseCategoryRepository.Commit();
        }
    }
}
