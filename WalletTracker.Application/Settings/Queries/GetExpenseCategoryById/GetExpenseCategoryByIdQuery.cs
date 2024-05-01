using MediatR;

namespace WalletTracker.Application.Settings.Queries.GetExpenseCategoryById
{
    public class GetExpenseCategoryByIdQuery : IRequest<ExpenseCategorySettingsDto>
    {
        public int Id { get; set; }
        public GetExpenseCategoryByIdQuery(int id)
        {
            Id = id;
        }
    }
}
