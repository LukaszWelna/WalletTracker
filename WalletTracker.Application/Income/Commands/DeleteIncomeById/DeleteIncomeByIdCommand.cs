using MediatR;

namespace WalletTracker.Application.Income.Commands.DeleteIncomeById
{
    public class DeleteIncomeByIdCommand : IRequest
    {
        public int IncomeId { get; set; }

        public DeleteIncomeByIdCommand(int incomeId)
        {
            IncomeId = incomeId;
        }
    }
}
