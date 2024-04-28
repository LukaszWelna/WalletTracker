using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Entities;

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
