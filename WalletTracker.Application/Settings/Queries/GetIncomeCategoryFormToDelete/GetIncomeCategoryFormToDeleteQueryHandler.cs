using AutoMapper;
using MediatR;
using WalletTracker.Application.Income;
using WalletTracker.Application.Settings.Commands.DeleteIncomeCategory;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Queries.GetIncomeCategoriesAssignedToLoggedUser
{
    public class GetIncomeCategoryFormToDeleteQueryHandler : IRequestHandler<GetIncomeCategoryFormToDeleteQuery, DeleteIncomeCategoryByIdCommand>
    {
        private readonly IIncomeCategoryRepository _incomeCategoryRepository;
        private readonly IMapper _mapper;

        public GetIncomeCategoryFormToDeleteQueryHandler(IIncomeCategoryRepository incomeCategoryRepository,
            IMapper mapper)
        {
            _incomeCategoryRepository = incomeCategoryRepository;
            _mapper = mapper;
        }

        // Return DeleteIncomeCategoryByIdCommand object with categories assigned to the logged user
        public async Task<DeleteIncomeCategoryByIdCommand> Handle(GetIncomeCategoryFormToDeleteQuery request, CancellationToken cancellationToken)
        {
            var categoriesAssignedToUser = await _incomeCategoryRepository
                    .GetCategoriesAssignedToLoggedUser();

            var categoryAssignedToUserDtos = _mapper.Map<List<IncomeCategoryAssignedToUserDto>>(categoriesAssignedToUser);

            var command = new DeleteIncomeCategoryByIdCommand()
            {
                UserCategoryDtos = categoryAssignedToUserDtos
            };

            return command;
        }
    }
}
