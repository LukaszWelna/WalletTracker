using MediatR;
using WalletTracker.Application.Income.Commands.CreateIncome;

namespace WalletTracker.Application.Income.Queries.GetCategoriesAssignedToLoggedUser
{
    public class GetDefaultIncomeFormDataQuery : IRequest<CreateIncomeCommand>
    {

    }
}
