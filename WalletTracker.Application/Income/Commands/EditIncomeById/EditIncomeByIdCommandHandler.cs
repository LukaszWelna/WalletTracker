using MediatR;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Income.Commands.EditIncomeById
{
    public class EditIncomeByIdCommandHandler : IRequestHandler<EditIncomeByIdCommand>
    {
        private readonly IIncomeRepository _incomeRepository;

        public EditIncomeByIdCommandHandler(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public async Task Handle(EditIncomeByIdCommand request, CancellationToken cancellationToken)
        {
            var income = await _incomeRepository.GetIncomeById(request.Id);

            // Edit current data by values specified in the view
            income.Amount = request.Amount;
            income.IncomeDate = request.IncomeDate;
            income.CategoryId = request.CategoryId;
            income.Comment = request.Comment;

            await _incomeRepository.Commit();
        }
    }
}
