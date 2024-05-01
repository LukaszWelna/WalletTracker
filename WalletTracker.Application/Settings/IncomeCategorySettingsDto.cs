using WalletTracker.Application.Income;

namespace WalletTracker.Application.Settings
{
    public class IncomeCategorySettingsDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<IncomeCategoryAssignedToUserDto> UserCategoryDtos { get; set; } = new List<IncomeCategoryAssignedToUserDto>();
    }
}
