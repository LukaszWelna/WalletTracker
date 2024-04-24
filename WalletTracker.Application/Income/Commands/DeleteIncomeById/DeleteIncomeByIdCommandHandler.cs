using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Income.Commands.DeleteIncomeById
{
    public class DeleteIncomeByIdCommandHandler : IRequestHandler<DeleteIncomeByIdCommand>
    {
        private readonly IIncomeRepository _incomeRepository;

        public DeleteIncomeByIdCommandHandler(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public async Task Handle(DeleteIncomeByIdCommand request, CancellationToken cancellationToken)
        {
            await _incomeRepository.DeleteIncomeById(request.IncomeId);
        }
    }
}
