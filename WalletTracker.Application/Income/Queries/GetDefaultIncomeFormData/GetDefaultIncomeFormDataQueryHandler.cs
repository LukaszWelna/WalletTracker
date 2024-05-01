using AutoMapper;
using MediatR;
using WalletTracker.Application.Income.Commands.CreateIncome;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Income.Queries.GetCategoriesAssignedToLoggedUser
{
    public class GetDefaultIncomeFormDataQueryHandler : IRequestHandler<GetDefaultIncomeFormDataQuery, CreateIncomeCommand>
    {
        private readonly IIncomeCategoryRepository _incomeCategoryRepository;
        private readonly IMapper _mapper;

        public GetDefaultIncomeFormDataQueryHandler(IIncomeCategoryRepository incomeCategoryRepository, IMapper mapper)
        {
            _incomeCategoryRepository = incomeCategoryRepository;
            _mapper = mapper;
        }

        public async Task<CreateIncomeCommand> Handle(GetDefaultIncomeFormDataQuery request, CancellationToken cancellationToken)
        {
            var categoriesAssignedToUser = await _incomeCategoryRepository
                .GetCategoriesAssignedToLoggedUser();

            var categoryAssignedToUserDtos = _mapper.Map<List<IncomeCategoryAssignedToUserDto>>(categoriesAssignedToUser);

            var command = new CreateIncomeCommand()
            {
                IncomeDate = DateOnly.FromDateTime(DateTime.UtcNow),
                UserCategoryDtos = categoryAssignedToUserDtos
            };

            return command;
        }
    }
}
