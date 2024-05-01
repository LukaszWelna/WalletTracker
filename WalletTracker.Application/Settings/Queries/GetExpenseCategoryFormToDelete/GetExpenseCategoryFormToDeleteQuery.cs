using MediatR;
using WalletTracker.Application.Settings.Commands.DeleteExpenseCategoryById;

namespace WalletTracker.Application.Settings.Queries.GetExpenseCategoryFormToDelete
{
    public class GetExpenseCategoryFormToDeleteQuery : IRequest<DeleteExpenseCategoryByIdCommand>
    {

    }
}
