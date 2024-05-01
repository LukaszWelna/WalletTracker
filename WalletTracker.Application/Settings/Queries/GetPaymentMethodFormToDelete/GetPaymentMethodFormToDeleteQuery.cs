using MediatR;
using WalletTracker.Application.Settings.Commands.DeletePaymentMethodById;

namespace WalletTracker.Application.Settings.Queries.GetPaymentMethodFormToDelete
{
    public class GetPaymentMethodFormToDeleteQuery : IRequest<DeletePaymentMethodByIdCommand>
    {

    }
}
