namespace WalletTracker.Application.Expense
{
    public class GetExpenseDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateOnly ExpenseDate { get; set; }
        public string CategoryName { get; set; } = default!;
        public string PaymentName { get; set; } = default!;
        public string? Comment { get; set; }
    }
}
