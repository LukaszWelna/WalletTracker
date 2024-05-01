namespace WalletTracker.Application.Expense
{
    public class CreateExpenseDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateOnly ExpenseDate { get; set; }
        public int CategoryId { get; set; }
        public List<ExpenseCategoryAssignedToUserDto> UserCategoryDtos { get; set; } = new List<ExpenseCategoryAssignedToUserDto>();
        public int PaymentId { get; set; }
        public List<PaymentMethodAssignedToUserDto> UserPaymentMethodDtos { get; set; } = new List<PaymentMethodAssignedToUserDto>();
        public string? Comment { get; set; }
    }
}
