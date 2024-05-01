namespace WalletTracker.Application.Income
{
    public class CreateIncomeDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateOnly IncomeDate { get; set; }
        public int CategoryId { get; set; }
        public List<IncomeCategoryAssignedToUserDto> UserCategoryDtos { get; set; } = new List<IncomeCategoryAssignedToUserDto>();
        public string? Comment { get; set; }
    }
}
