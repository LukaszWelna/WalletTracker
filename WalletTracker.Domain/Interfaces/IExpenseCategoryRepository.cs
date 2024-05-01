using WalletTracker.Domain.Entities;

namespace WalletTracker.Domain.Interfaces
{
    public interface IExpenseCategoryRepository
    {
        public Task<IEnumerable<ExpenseCategoryDefault>> GetDefaultCategories();
        public Task SeedDefaultCategoriesToUser(IEnumerable<ExpenseCategoryAssignedToUser> expenseCategoriesAssignedToUser);
        public Task<IEnumerable<ExpenseCategoryAssignedToUser>> GetCategoriesAssignedToLoggedUser();
        public Task Create(ExpenseCategoryAssignedToUser category);
        public Task DeleteById(int id);
        public Task Commit();
        public Task<ExpenseCategoryAssignedToUser?> GetByName(string name);
        public Task<ExpenseCategoryAssignedToUser> GetById(int id);
    }
}
