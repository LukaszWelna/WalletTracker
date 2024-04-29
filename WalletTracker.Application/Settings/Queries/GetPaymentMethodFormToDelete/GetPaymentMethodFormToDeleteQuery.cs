using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Settings.Commands.DeletePaymentMethodById;

namespace WalletTracker.Application.Settings.Queries.GetPaymentMethodFormToDelete
{
    public class GetPaymentMethodFormToDeleteQuery : IRequest<DeletePaymentMethodByIdCommand>
    {

    }
}
