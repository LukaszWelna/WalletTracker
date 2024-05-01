using WalletTracker.Domain.Models;

namespace WalletTracker.Domain.Entities
{
    public class IncomeCategoryAssignedToUser
    {
        public int Id { get; set; }
        public string UserId { get; set; } = default!;
        public ApplicationUser User { get; set; } = default!;
        public string Name { get; set; } = default!;
        public List<Income> Incomes { get; set; } = new List<Income>();
    }
}
