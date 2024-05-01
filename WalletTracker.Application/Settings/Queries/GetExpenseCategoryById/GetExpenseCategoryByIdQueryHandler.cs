using AutoMapper;
using MediatR;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Queries.GetExpenseCategoryById
{
    public class GetExpenseCategoryByIdQueryHandler : IRequestHandler<GetExpenseCategoryByIdQuery, ExpenseCategorySettingsDto>
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IMapper _mapper;

        public GetExpenseCategoryByIdQueryHandler(IExpenseCategoryRepository expenseCategoryRepository, IMapper mapper)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
            _mapper = mapper;
        }

        public async Task<ExpenseCategorySettingsDto> Handle(GetExpenseCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var expenseCategory = await _expenseCategoryRepository.GetById(request.Id);

            var expenseCategorySettingsDto = _mapper.Map<ExpenseCategorySettingsDto>(expenseCategory);

            return expenseCategorySettingsDto;
        }
    }
}
