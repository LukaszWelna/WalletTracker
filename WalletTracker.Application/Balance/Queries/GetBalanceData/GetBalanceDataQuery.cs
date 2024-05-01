using MediatR;

namespace WalletTracker.Application.Balance.Queries.GetBalanceData
{
    public class GetBalanceDataQuery : IRequest<BalanceDto>
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
