using MediatR;

namespace WalletTracker.Application.Expense.Queries.GetMoneySpentData
{
    public class GetMoneySpentDataQuery : IRequest<decimal>
    {
        public int CategoryId { get; set; }
        public DateOnly Date { get; set; }

        public GetMoneySpentDataQuery(int categoryId, DateOnly date)
        {
            CategoryId = categoryId;
            Date = date;
        }
    }
}
