using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Settings;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Expense.Queries.GetCategoryLimitData
{
    public class GetCategoryLimitDataQueryHandler : IRequestHandler<GetCategoryLimitDataQuery, CategoryLimitDto>
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryLimitDataQueryHandler(IExpenseCategoryRepository expenseCategoryRepository, IMapper mapper)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryLimitDto> Handle(GetCategoryLimitDataQuery request, CancellationToken cancellationToken)
        {
            var expenseCategory = await _expenseCategoryRepository.GetById(request.Id);

            var categoryLimitDto = _mapper.Map<CategoryLimitDto>(expenseCategory);

            return categoryLimitDto;
        }
    }
}
