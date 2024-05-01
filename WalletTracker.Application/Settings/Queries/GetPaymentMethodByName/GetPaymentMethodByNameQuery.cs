using MediatR;
using WalletTracker.Domain.Entities;

namespace WalletTracker.Application.Settings.Queries.GetPaymentMethodByName
{
    public class GetPaymentMethodByNameQuery : IRequest<PaymentMethodAssignedToUser?>
    {
        public string Name { get; set; }
        public GetPaymentMethodByNameQuery(string name)
        {
            Name = name;
        }
    }
}
