using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Settings.Commands.DeleteExpenseCategoryById;

namespace WalletTracker.Application.Settings.Queries.GetExpenseCategoryFormToDelete
{
    public class GetExpenseCategoryFormToDeleteQuery : IRequest<DeleteExpenseCategoryByIdCommand>
    {

    }
}
