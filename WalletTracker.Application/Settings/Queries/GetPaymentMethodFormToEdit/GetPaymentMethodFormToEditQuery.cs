using MediatR;
using WalletTracker.Application.Settings.Commands.EditPaymentMethodById;

namespace WalletTracker.Application.Settings.Queries.GetPaymentMethodFormToEdit
{
    public class GetPaymentMethodFormToEditQuery : IRequest<EditPaymentMethodByIdCommand>
    {

    }
}
