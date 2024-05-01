using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
