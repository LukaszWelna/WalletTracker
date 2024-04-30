using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Models;

namespace WalletTracker.Domain.Interfaces
{
    public interface IIncomeRepository
    {
        public Task Create(Income income);
        public Task<IEnumerable<IEnumerable<Income>>> GetUserIncomesFromPeriod(DateOnly startDate, DateOnly endDate);
        public Task DeleteIncomeById (int incomeId);
        public Task<Income> GetIncomeById (int incomeId);
        public Task Commit();
        public Task<IEnumerable<IncomeTotalAmountInCategoryDto>> GetTotalAmountInCategories(DateOnly startDate, DateOnly endDate);
    }
}
