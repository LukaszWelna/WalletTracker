using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Settings.Commands.EditExpenseCategoryById;

namespace WalletTracker.Application.Settings.Queries.GetExpenseCategoryFormToEdit
{
    public class GetExpenseCategoryFormToEditQuery : IRequest<EditExpenseCategoryByIdCommand>
    {

    }
}
