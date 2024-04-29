using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Settings.Commands.EditPaymentMethodById;

namespace WalletTracker.Application.Settings.Queries.GetPaymentMethodFormToEdit
{
    public class GetPaymentMethodFormToEditQuery : IRequest<EditPaymentMethodByIdCommand>
    {

    }
}
