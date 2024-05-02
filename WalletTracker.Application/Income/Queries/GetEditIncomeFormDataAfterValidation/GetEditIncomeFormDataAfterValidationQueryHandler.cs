using AutoMapper;
using MediatR;
using WalletTracker.Application.Income.Commands.EditIncomeById;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Income.Queries.GetEditIncomeFormDataAfterValidation
{
    public class GetEditIncomeFormDataAfterValidationQueryHandler : IRequestHandler<GetEditIncomeFormDataAfterValidationQuery, EditIncomeByIdCommand>
    {
        private readonly IIncomeCategoryRepository _incomeCategoryRepository;
        private readonly IMapper _mapper;

        public GetEditIncomeFormDataAfterValidationQueryHandler(IIncomeCategoryRepository incomeCategoryRepository, IMapper mapper)
        {
            _incomeCategoryRepository = incomeCategoryRepository;
            _mapper = mapper;
        }

        // Return EditIncomeByIdCommand object with categories assigned to the logged user
        public async Task<EditIncomeByIdCommand> Handle(GetEditIncomeFormDataAfterValidationQuery request, CancellationToken cancellationToken)
        {
            var categoriesAssignedToUser = await _incomeCategoryRepository
                .GetCategoriesAssignedToLoggedUser();

            var categoryAssignedToUserDtos = _mapper.Map<List<IncomeCategoryAssignedToUserDto>>(categoriesAssignedToUser);

            request.EditIncomeByIdCommand.UserCategoryDtos = categoryAssignedToUserDtos;

            return request.EditIncomeByIdCommand;
        }
    }
}
