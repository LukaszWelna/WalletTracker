using WalletTracker.Application.Expense;
using WalletTracker.Application.Income;

namespace WalletTracker.MVC.Models
{
    public class BalanceModel
    {
        public IEnumerable<IEnumerable<GetIncomeDto>> Incomes { get; set; } = new List<List<GetIncomeDto>>();
        public IEnumerable<IEnumerable<GetExpenseDto>> Expenses { get; set; } = new List<List<GetExpenseDto>>();
        public CreateIncomeDto IncomeDto { get; set; } = new CreateIncomeDto();
    }
}
