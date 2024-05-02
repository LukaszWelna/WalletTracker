using AutoMapper;
using MediatR;
using WalletTracker.Application.Income;
using WalletTracker.Application.Settings.Commands.EditIncomeCategoryById;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Queries.GetIncomeCategoryFormToEdit
{
    public class GetIncomeCategoryFormToEditQueryHandler : IRequestHandler<GetIncomeCategoryFormToEditQuery, EditIncomeCategoryByIdCommand>
    {
        private readonly IIncomeCategoryRepository _incomeCategoryRepository;
        private readonly IMapper _mapper;

        public GetIncomeCategoryFormToEditQueryHandler(IIncomeCategoryRepository incomeCategoryRepository, IMapper mapper)
        {
            _incomeCategoryRepository = incomeCategoryRepository;
            _mapper = mapper;
        }

        // Return EditIncomeCategoryByIdCommand object with categories assigned to the logged user
        public async Task<EditIncomeCategoryByIdCommand> Handle(GetIncomeCategoryFormToEditQuery request, CancellationToken cancellationToken)
        {
            var categoriesAssignedToUser = await _incomeCategoryRepository
                    .GetCategoriesAssignedToLoggedUser();

            var categoryAssignedToUserDtos = _mapper.Map<List<IncomeCategoryAssignedToUserDto>>(categoriesAssignedToUser);

            var command = new EditIncomeCategoryByIdCommand()
            {
                UserCategoryDtos = categoryAssignedToUserDtos
            };

            return command;
        }
    }
}
