using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Entities;

namespace WalletTracker.Domain.Interfaces
{
    public interface IExpenseCategoryRepository
    {
        public Task Create(ExpenseCategoryAssignedToUser category);
        public Task Delete(int categoryId);
        public Task Commit();
    }
}
