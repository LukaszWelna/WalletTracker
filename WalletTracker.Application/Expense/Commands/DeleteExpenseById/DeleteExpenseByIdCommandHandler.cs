using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Expense.Commands.DeleteExpenseById
{
    public class DeleteExpenseByIdCommandHandler : IRequestHandler<DeleteExpenseByIdCommand>
    {
        private readonly IExpenseRepository _expenseRepository;

        public DeleteExpenseByIdCommandHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task Handle(DeleteExpenseByIdCommand request, CancellationToken cancellationToken)
        {
            await _expenseRepository.DeleteExpenseById(request.ExpenseId);
        }
    }
}
