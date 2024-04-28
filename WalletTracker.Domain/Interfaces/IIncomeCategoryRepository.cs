using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Entities;

namespace WalletTracker.Domain.Interfaces
{
    public interface IIncomeCategoryRepository
    {
        public Task<IEnumerable<IncomeCategoryDefault>> GetDefaultCategories();
        public Task SeedDefaultCategoriesToUser(IEnumerable<IncomeCategoryAssignedToUser> incomeCategoriesAssignedToUser);
        public Task<IEnumerable<IncomeCategoryAssignedToUser>> GetCategoriesAssignedToLoggedUser();
        public Task Create(IncomeCategoryAssignedToUser category);
        public Task DeleteById(int id);
        public Task Commit();
        public Task<IncomeCategoryAssignedToUser?> GetByName(string name);
    }
}
