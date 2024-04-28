using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Income.Commands.CreateIncome;
using WalletTracker.Application.Income;
using WalletTracker.Domain.Interfaces;
using AutoMapper;
using WalletTracker.Application.Settings.Commands.DeleteIncomeCategory;

namespace WalletTracker.Application.Settings.Queries.GetIncomeCategoriesAssignedToLoggedUser
{
    public class GetIncomeCategoryFormToDeleteQueryHandler : IRequestHandler<GetIncomeCategoryFormToDeleteQuery, DeleteIncomeCategoryByIdCommand>
    {
        private readonly IIncomeCategoryRepository _incomeCategoryRepository;
        private readonly IMapper _mapper;

        public GetIncomeCategoryFormToDeleteQueryHandler(IIncomeCategoryRepository incomeCategoryRepository,
            IMapper mapper)
        {
            _incomeCategoryRepository = incomeCategoryRepository;
            _mapper = mapper;
        }

        public async Task<DeleteIncomeCategoryByIdCommand> Handle(GetIncomeCategoryFormToDeleteQuery request, CancellationToken cancellationToken)
        {
            var categoriesAssignedToUser = await _incomeCategoryRepository
                    .GetCategoriesAssignedToLoggedUser();

            var categoryAssignedToUserDtos = _mapper.Map<List<IncomeCategoryAssignedToUserDto>>(categoriesAssignedToUser);

            var command = new DeleteIncomeCategoryByIdCommand()
            {
                UserCategoryDtos = categoryAssignedToUserDtos
            };

            return command;
        }
    }
}
