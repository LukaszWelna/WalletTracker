using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Models;

namespace WalletTracker.Domain.Interfaces
{
    public interface IExpenseRepository
    {
        public Task Create(Expense expense);
        public Task<IEnumerable<IEnumerable<Expense>>> GetUserExpensesFromPeriod(DateOnly startDate, DateOnly endDate);
        public Task DeleteExpenseById(int expenseId);
        public Task<Expense> GetExpenseById(int expenseId);
        public Task Commit();
        public Task<IEnumerable<ExpenseTotalAmountInCategoryDto>> GetTotalAmountInCategories(DateOnly startDate, DateOnly endDate);
        public decimal GetMoneySpent(int categoryId, DateOnly firstDayOfMonth, DateOnly lastDayOfMonth);
    }
}
