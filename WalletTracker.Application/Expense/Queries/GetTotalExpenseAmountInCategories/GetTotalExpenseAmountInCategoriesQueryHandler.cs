using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Interfaces;
using WalletTracker.Domain.Models;

namespace WalletTracker.Application.Expense.Queries.GetTotalAmountInCategories
{
    public class GetTotalExpenseAmountInCategoriesQueryHandler : IRequestHandler<GetTotalExpenseAmountInCategoriesQuery, IEnumerable<ExpenseTotalAmountInCategoryDto>>
    {
        private readonly IExpenseRepository _expenseRepository;

        public GetTotalExpenseAmountInCategoriesQueryHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<IEnumerable<ExpenseTotalAmountInCategoryDto>> Handle(GetTotalExpenseAmountInCategoriesQuery request, CancellationToken cancellationToken)
        {
            var totalAmountInCategories = await _expenseRepository.GetTotalAmountInCategories();

            return totalAmountInCategories;
        }
    }
}
