using WalletTracker.Application.Expense;

namespace WalletTracker.Application.Settings
{
    public class PaymentMethodSettingsDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<PaymentMethodAssignedToUserDto> UserPaymentMethodDtos { get; set; } = new List<PaymentMethodAssignedToUserDto>();
    }
}
