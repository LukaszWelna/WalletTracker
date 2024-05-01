using WalletTracker.Domain.Models;

namespace WalletTracker.Domain.Entities
{
    public class Income
    {
        public int Id { get; set; }
        public string UserId { get; set; } = default!;
        public ApplicationUser User { get; set; } = default!;
        public int CategoryId { get; set; }
        public IncomeCategoryAssignedToUser Category { get; set; } = default!;
        public decimal Amount { get; set; }
        public DateOnly IncomeDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? Comment { get; set; }
    }
}
