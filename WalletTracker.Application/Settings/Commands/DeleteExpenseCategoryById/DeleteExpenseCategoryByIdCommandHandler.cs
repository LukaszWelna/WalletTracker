using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Settings.Commands.DeleteExpenseCategoryById
{
    public class DeleteExpenseCategoryByIdCommandHandler : IRequestHandler<DeleteExpenseCategoryByIdCommand>
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;

        public DeleteExpenseCategoryByIdCommandHandler(IExpenseCategoryRepository expenseCategoryRepository)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
        }

        public async Task Handle(DeleteExpenseCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            await _expenseCategoryRepository.DeleteById(request.Id);
        }
    }
}
