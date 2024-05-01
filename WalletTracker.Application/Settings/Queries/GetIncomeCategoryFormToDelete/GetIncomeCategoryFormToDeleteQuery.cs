using MediatR;
using WalletTracker.Application.Settings.Commands.DeleteIncomeCategory;

namespace WalletTracker.Application.Settings.Queries.GetIncomeCategoriesAssignedToLoggedUser
{
    public class GetIncomeCategoryFormToDeleteQuery : IRequest<DeleteIncomeCategoryByIdCommand>
    {

    }
}
