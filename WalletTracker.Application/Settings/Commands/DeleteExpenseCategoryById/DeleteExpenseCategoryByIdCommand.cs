using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTracker.Application.Settings.Commands.DeleteExpenseCategoryById
{
    public class DeleteExpenseCategoryByIdCommand : ExpenseCategorySettingsDto, IRequest
    {

    }
}
