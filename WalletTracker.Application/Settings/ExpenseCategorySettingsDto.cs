using WalletTracker.Application.Expense;

namespace WalletTracker.Application.Settings
{
    public class ExpenseCategorySettingsDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool LimitIsActive { get; set; }
        public decimal Limit { get; set; }
        public List<ExpenseCategoryAssignedToUserDto> UserCategoryDtos { get; set; } = new List<ExpenseCategoryAssignedToUserDto>();
    }
}
