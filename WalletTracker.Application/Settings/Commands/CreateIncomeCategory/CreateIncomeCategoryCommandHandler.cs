using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.ApplicationUser;
using WalletTracker.Application.Settings.Commands.CreateIncomeCategory;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Commands.CreateCategory
{
    public class CreateIncomeCategoryCommandHandler : IRequestHandler<CreateIncomeCategoryCommand>
    {
        private readonly IIncomeCategoryRepository _incomeCategoryRepository;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public CreateIncomeCategoryCommandHandler(IIncomeCategoryRepository incomeCategoryRepository, 
            IUserContextService userContextService,
            IMapper mapper)
        {
            _incomeCategoryRepository = incomeCategoryRepository;
            _userContextService = userContextService;
            _mapper = mapper;
        }
        public async Task Handle(CreateIncomeCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<IncomeCategoryAssignedToUser>(request);

            category.UserId = _userContextService.GetCurrentUser().Id;

            await _incomeCategoryRepository.Create(category);
        }
    }
}
