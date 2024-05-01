namespace WalletTracker.Application.Balance
{
    public class BalanceCanvasDto
    {
        // Category name - Label name needed to canvas pie chart
        public string label { get; set; } = default!;
        // Total amount - Y name needed to canvas pie chart
        public decimal y { get; set; }
    }
}
