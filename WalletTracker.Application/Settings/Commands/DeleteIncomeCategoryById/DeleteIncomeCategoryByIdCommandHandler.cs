using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Income.Commands.DeleteIncomeById;
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
