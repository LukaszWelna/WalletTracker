using MediatR;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Queries.GetIncomeCategoryByName
{
    public class GetIncomeCategoryByNameQueryHandler : IRequestHandler<GetIncomeCategoryByNameQuery, IncomeCategoryAssignedToUser?>
    {
        private readonly IIncomeCategoryRepository _incomeCategoryRepository;

        public GetIncomeCategoryByNameQueryHandler(IIncomeCategoryRepository incomeCategoryRepository)
        {
            _incomeCategoryRepository = incomeCategoryRepository;
        }

        public async Task<IncomeCategoryAssignedToUser?> Handle(GetIncomeCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            var category = await _incomeCategoryRepository.GetByName(request.Name);

            return category;
        }
    }
}
