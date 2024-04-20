using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Income.Commands.CreateIncome;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Income.Queries.GetCategoriesAssignedToLoggedUser
{
    public class GetDefaultIncomeFormDataQueryHandler : IRequestHandler<GetDefaultIncomeFormDataQuery, CreateIncomeCommand>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IMapper _mapper;

        public GetDefaultIncomeFormDataQueryHandler(IIncomeRepository incomeRepository, IMapper mapper)
        {
            _incomeRepository = incomeRepository;
            _mapper = mapper;
        }

        public async Task<CreateIncomeCommand> Handle(GetDefaultIncomeFormDataQuery request, CancellationToken cancellationToken)
        {
            var categoriesAssignedToUser = await _incomeRepository
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
