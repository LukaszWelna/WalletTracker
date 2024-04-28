using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Income.Commands.SeedIncomeCategories
{
    public class SeedIncomeCategoriesToNewUserCommandHandler : IRequestHandler<SeedIncomeCategoriesToNewUserCommand>
    {
        private readonly IIncomeCategoryRepository _incomeCategoryRepository;

        public SeedIncomeCategoriesToNewUserCommandHandler(IIncomeCategoryRepository incomeCategoryRepository)
        {
            _incomeCategoryRepository = incomeCategoryRepository;
        }

        public async Task Handle(SeedIncomeCategoriesToNewUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
            {
                throw new InvalidOperationException("User Id cannot be null or empty");
            }

            var incomeCategoriesAssignedToUserId = new List<IncomeCategoryAssignedToUser>();
            var incomeCategoriesDefault = await _incomeCategoryRepository.GetDefaultCategories();

            foreach (var category in incomeCategoriesDefault)
            {
                incomeCategoriesAssignedToUserId.Add(
                    new IncomeCategoryAssignedToUser()
                    {
                        UserId = request.UserId,
                        Name = category.Name
                    });
            }

            await _incomeCategoryRepository.SeedDefaultCategoriesToUser(incomeCategoriesAssignedToUserId);
        }
    }
}
