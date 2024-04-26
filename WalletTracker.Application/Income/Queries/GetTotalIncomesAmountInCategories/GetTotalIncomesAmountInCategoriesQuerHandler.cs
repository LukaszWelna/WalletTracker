using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Interfaces;
using WalletTracker.Domain.Models;

namespace WalletTracker.Application.Income.Queries.GetTotalAmountInCategories
{
    public class GetTotalIncomesAmountInCategoriesQuerHandler : IRequestHandler<GetTotalIncomesAmountInCategoriesQuery, IEnumerable<IncomeTotalAmountInCategoryDto>>
    {
        private readonly IIncomeRepository _incomeRepository;

        public GetTotalIncomesAmountInCategoriesQuerHandler(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public async Task<IEnumerable<IncomeTotalAmountInCategoryDto>> Handle(GetTotalIncomesAmountInCategoriesQuery request, CancellationToken cancellationToken)
        {
            var totalAmountInCategories = await _incomeRepository.GetTotalAmountInCategories();

            return totalAmountInCategories;
        }
    }
}
