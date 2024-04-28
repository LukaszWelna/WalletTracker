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
    public class GetIncomeFormDataAfterValidationQueryHandler : IRequestHandler<GetIncomeFormDataAfterValidationQuery, CreateIncomeCommand>
    {
        private readonly IIncomeCategoryRepository _incomeCategoryRepository;
        private readonly IMapper _mapper;

        public GetIncomeFormDataAfterValidationQueryHandler(IIncomeCategoryRepository incomeCategoryRepository, IMapper mapper)
        {
            _incomeCategoryRepository = incomeCategoryRepository;
            _mapper = mapper;
        }
        public async Task<CreateIncomeCommand> Handle(GetIncomeFormDataAfterValidationQuery request, CancellationToken cancellationToken)
        {
            var categoriesAssignedToUser = await _incomeCategoryRepository
                .GetCategoriesAssignedToLoggedUser();

            var categoryAssignedToUserDtos = _mapper.Map<List<IncomeCategoryAssignedToUserDto>>(categoriesAssignedToUser);

            request.CreateIncomeCommand.UserCategoryDtos = categoryAssignedToUserDtos;

            return request.CreateIncomeCommand;
        }
    }
}
