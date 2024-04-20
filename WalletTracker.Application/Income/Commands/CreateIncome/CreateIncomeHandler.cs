using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.ApplicationUser;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Income.Commands.CreateIncome
{
    public class CreateIncomeCommandHandler : IRequestHandler<CreateIncomeCommand>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public CreateIncomeCommandHandler(IIncomeRepository incomeRepository, IMapper mapper,
            IUserContextService userContextService)
        {
            _incomeRepository = incomeRepository;
            _mapper = mapper;
            _userContextService = userContextService;
        }
        public async Task Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
        {
            var income = _mapper.Map<Domain.Entities.Income>(request);

            income.UserId = _userContextService.GetCurrentUser().Id;

            await _incomeRepository.Create(income);
        }
    }
}
