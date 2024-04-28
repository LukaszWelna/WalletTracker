using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Expense;
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
