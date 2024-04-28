using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Settings.Commands.EditIncomeCategoryById;

namespace WalletTracker.Application.Settings.Queries.GetIncomeCategoryFormToEdit
{
    public class GetIncomeCategoryFormToEditQuery : IRequest<EditIncomeCategoryByIdCommand>
    {

    }
}
