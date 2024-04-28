using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Settings.Commands.DeleteIncomeCategory;

namespace WalletTracker.Application.Settings.Queries.GetIncomeCategoriesAssignedToLoggedUser
{
    public class GetIncomeCategoryFormToDeleteQuery : IRequest<DeleteIncomeCategoryByIdCommand>
    {

    }
}
