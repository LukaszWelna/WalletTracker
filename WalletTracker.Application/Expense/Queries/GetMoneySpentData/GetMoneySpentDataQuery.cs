using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
