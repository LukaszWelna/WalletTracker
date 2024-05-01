using AutoMapper;
using MediatR;
using WalletTracker.Application.ApplicationUser;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Commands.CreateExpenseCategory
{
    public class CreateExpenseCategoryCommandHandler : IRequestHandler<CreateExpenseCategoryCommand>
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public CreateExpenseCategoryCommandHandler(IExpenseCategoryRepository expenseCategoryRepository,
            IMapper mapper, IUserContextService userContextService)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task Handle(CreateExpenseCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<ExpenseCategoryAssignedToUser>(request);

            category.UserId = _userContextService.GetCurrentUser().Id;

            await _expenseCategoryRepository.Create(category);
        }
    }
}
