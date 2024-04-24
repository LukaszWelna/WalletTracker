using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Income.Commands.EditIncomeById;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Income.Queries.GetEditIncomeFormDataAfterValidation
{
    public class GetEditIncomeFormDataAfterValidationQueryHandler : IRequestHandler<GetEditIncomeFormDataAfterValidationQuery, EditIncomeByIdCommand>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IMapper _mapper;

        public GetEditIncomeFormDataAfterValidationQueryHandler(IIncomeRepository incomeRepository, IMapper mapper)
        {
            _incomeRepository = incomeRepository;
            _mapper = mapper;
        }

        public async Task<EditIncomeByIdCommand> Handle(GetEditIncomeFormDataAfterValidationQuery request, CancellationToken cancellationToken)
        {
            var categoriesAssignedToUser = await _incomeRepository
                .GetCategoriesAssignedToLoggedUser();

            var categoryAssignedToUserDtos = _mapper.Map<List<IncomeCategoryAssignedToUserDto>>(categoriesAssignedToUser);

            request.EditIncomeByIdCommand.UserCategoryDtos = categoryAssignedToUserDtos;

            return request.EditIncomeByIdCommand;
        }
    }
}
