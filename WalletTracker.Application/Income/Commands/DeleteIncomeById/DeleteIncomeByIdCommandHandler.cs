using MediatR;
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
            if (request.IncomeId <= 0)
            {
                throw new InvalidOperationException("Incorrect id value.");
            }

            await _incomeRepository.DeleteIncomeById(request.IncomeId);
        }
    }
}
