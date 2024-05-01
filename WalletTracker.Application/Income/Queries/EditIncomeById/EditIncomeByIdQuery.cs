using MediatR;
using WalletTracker.Application.Income.Commands.EditIncomeById;

namespace WalletTracker.Application.Income.Queries.EditIncomeById
{
    public class EditIncomeByIdQuery : IRequest<EditIncomeByIdCommand>
    {
        public int IncomeId { get; set; }

        public EditIncomeByIdQuery(int incomeId)
        {
            IncomeId = incomeId;
        }
    }
}
