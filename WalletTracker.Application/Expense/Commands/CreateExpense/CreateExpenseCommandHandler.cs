using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.ApplicationUser;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Expense.Commands.CreateExpense
{
    public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public CreateExpenseCommandHandler(IExpenseRepository expenseRepository, IMapper mapper,
            IUserContextService userContextService)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            var expense = _mapper.Map<Domain.Entities.Expense>(request);

            expense.UserId = _userContextService.GetCurrentUser().Id;

            await _expenseRepository.Create(expense);
        }
    }
}
