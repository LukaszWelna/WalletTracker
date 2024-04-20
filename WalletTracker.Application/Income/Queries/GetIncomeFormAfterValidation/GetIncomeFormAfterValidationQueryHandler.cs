using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Income.Commands.CreateIncome;
using WalletTracker.Application.Income.Queries.GetCategoriesAssignedToLoggedUse;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Income.Queries.GetCategoriesAssignedToUser
{
    public class GetIncomeFormAfterValidationQueryHandler : IRequestHandler<GetIncomeFormAfterValidationQuery, CreateIncomeCommand>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IMapper _mapper;

        public GetIncomeFormAfterValidationQueryHandler(IIncomeRepository incomeRepository, IMapper mapper)
        {
            _incomeRepository = incomeRepository;
            _mapper = mapper;
        }
        public async Task<CreateIncomeCommand> Handle(GetIncomeFormAfterValidationQuery request, CancellationToken cancellationToken)
        {
            var categoriesAssignedToUser = await _incomeRepository
                .GetCategoriesAssignedToLoggedUser();

            var categoryAssignedToUserDtos = _mapper.Map<List<IncomeCategoryAssignedToUserDto>>(categoriesAssignedToUser);

            request.CreateIncomeCommand.UserCategoryDtos = categoryAssignedToUserDtos;

            return request.CreateIncomeCommand;
        }
    }
}
