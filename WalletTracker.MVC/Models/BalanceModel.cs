using WalletTracker.Application.Balance;
using WalletTracker.Application.Expense;
using WalletTracker.Application.Income;
using WalletTracker.Domain.Models;

namespace WalletTracker.MVC.Models
{
    public class BalanceModel
    {
        public IEnumerable<IEnumerable<GetIncomeDto>> Incomes { get; set; } = new List<List<GetIncomeDto>>();
        public IEnumerable<IEnumerable<GetExpenseDto>> Expenses { get; set; } = new List<List<GetExpenseDto>>();
        public IEnumerable<IncomeTotalAmountInCategoryDto> IncomeTotalAmountInCategories { get; set; } = new List<IncomeTotalAmountInCategoryDto>();
        public IEnumerable<ExpenseTotalAmountInCategoryDto> ExpenseTotalAmountInCategories { get; set; } = new List<ExpenseTotalAmountInCategoryDto>();
        public decimal TotalIncomesAmount { get; set; }
        public decimal TotalExpensesAmount { get; set; }
        public IEnumerable<BalanceDto> BalanceDtos { get; set; } = new List<BalanceDto>();
    }
}
