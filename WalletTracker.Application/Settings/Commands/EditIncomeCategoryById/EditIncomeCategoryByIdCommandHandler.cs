using MediatR;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Commands.EditIncomeCategoryById
{
    public class EditIncomeCategoryByIdCommandHandler : IRequestHandler<EditIncomeCategoryByIdCommand>
    {
        private readonly IIncomeCategoryRepository _incomeCategoryRepository;

        public EditIncomeCategoryByIdCommandHandler(IIncomeCategoryRepository incomeCategoryRepository)
        {
            _incomeCategoryRepository = incomeCategoryRepository;
        }

        public async Task Handle(EditIncomeCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            var incomeCategory = await _incomeCategoryRepository.GetById(request.Id);

            incomeCategory.Name = request.Name!;

            await _incomeCategoryRepository.Commit();
        }
    }
}
