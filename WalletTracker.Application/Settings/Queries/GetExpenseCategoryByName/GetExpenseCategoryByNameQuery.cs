using MediatR;
using WalletTracker.Domain.Entities;

namespace WalletTracker.Application.Settings.Queries.GetExpenseCategoryByName
{
    public class GetExpenseCategoryByNameQuery : IRequest<ExpenseCategoryAssignedToUser?>
    {
        public string Name { get; set; }
        public GetExpenseCategoryByNameQuery(string name)
        {
            Name = name;
        }
    }
}
