using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Entities;

namespace WalletTracker.Domain.Interfaces
{
    public interface IIncomeRepository
    {
        public Task Create(Income income);
        public Task<List<IncomeCategoryDefault>> GetDefaultCategories();
        public Task SeedDefaultCategoriesToUser(List<IncomeCategoryAssignedToUser> incomeCategoriesAssignedToUser);
        public Task<List<IncomeCategoryAssignedToUser>> GetCategoriesAssignedToLoggedUser();
    }
}
