using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Models;

namespace WalletTracker.Application.Balance.Queries.GetBalanceData
{
    public class GetBalanceDataQuery : IRequest<IEnumerable<BalanceDto>>
    {
        public IEnumerable<ExpenseTotalAmountInCategoryDto> ExpenseTotalAmountInCategories { get; set; }

        public GetBalanceDataQuery(IEnumerable<ExpenseTotalAmountInCategoryDto> expenseTotalAmountInCategories)
        {
            ExpenseTotalAmountInCategories = expenseTotalAmountInCategories;
        }
    }
}
