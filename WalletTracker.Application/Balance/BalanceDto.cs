using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Expense;
using WalletTracker.Application.Income;
using WalletTracker.Domain.Models;

namespace WalletTracker.Application.Balance
{
    public class BalanceDto
    {
        public IEnumerable<IEnumerable<GetIncomeDto>> Incomes { get; set; } = new List<List<GetIncomeDto>>();
        public IEnumerable<IEnumerable<GetExpenseDto>> Expenses { get; set; } = new List<List<GetExpenseDto>>();
        public IEnumerable<IncomeTotalAmountInCategoryDto> IncomeTotalAmountInCategories { get; set; } = new List<IncomeTotalAmountInCategoryDto>();
        public IEnumerable<ExpenseTotalAmountInCategoryDto> ExpenseTotalAmountInCategories { get; set; } = new List<ExpenseTotalAmountInCategoryDto>();
        public decimal TotalIncomesAmount { get; set; }
        public decimal TotalExpensesAmount { get; set; }
        public IEnumerable<BalanceCanvasDto> BalanceCanvasDtos { get; set; } = new List<BalanceCanvasDto>();
        public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
        public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
