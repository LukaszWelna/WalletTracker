using MediatR;

namespace WalletTracker.Application.Expense.Queries.GetCategoryLimitData
{
    public class GetCategoryLimitDataQuery : IRequest<CategoryLimitDto>
    {
        public int Id { get; set; }

        public GetCategoryLimitDataQuery(int id)
        {
            Id = id;
        }
    }
}
