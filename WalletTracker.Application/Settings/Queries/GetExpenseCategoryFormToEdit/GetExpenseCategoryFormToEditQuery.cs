using MediatR;
using WalletTracker.Application.Settings.Commands.EditExpenseCategoryById;

namespace WalletTracker.Application.Settings.Queries.GetExpenseCategoryFormToEdit
{
    public class GetExpenseCategoryFormToEditQuery : IRequest<EditExpenseCategoryByIdCommand>
    {

    }
}
