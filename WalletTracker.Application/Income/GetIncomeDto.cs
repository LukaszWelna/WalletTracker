namespace WalletTracker.Application.Income
{
    public class GetIncomeDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateOnly IncomeDate { get; set; }
        public string CategoryName { get; set; } = default!;
        public string? Comment { get; set; }
    }
}
