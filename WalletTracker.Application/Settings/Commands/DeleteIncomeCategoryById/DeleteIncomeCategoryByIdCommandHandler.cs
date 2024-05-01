using MediatR;
using WalletTracker.Application.Settings.Commands.DeleteIncomeCategory;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Commands.DeleteIncomeCategoryById
{
    public class DeleteIncomeCategoryByIdCommandHandler : IRequestHandler<DeleteIncomeCategoryByIdCommand>
    {
        private readonly IIncomeCategoryRepository _incomeCategoryRepository;

        public DeleteIncomeCategoryByIdCommandHandler(IIncomeCategoryRepository incomeCategoryRepository)
        {
            _incomeCategoryRepository = incomeCategoryRepository;
        }

        public async Task Handle(DeleteIncomeCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            await _incomeCategoryRepository.DeleteById(request.Id);
        }
    }
}
