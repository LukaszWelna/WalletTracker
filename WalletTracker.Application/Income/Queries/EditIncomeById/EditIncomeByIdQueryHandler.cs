using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Income.Commands.CreateIncome;
using WalletTracker.Application.Income.Commands.EditIncomeById;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Income.Queries.EditIncomeById
{
    public class EditIncomeByIdQueryHandler : IRequestHandler<EditIncomeByIdQuery, EditIncomeByIdCommand>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IIncomeCategoryRepository _incomeCategoryRepository;
        private readonly IMapper _mapper;

        public EditIncomeByIdQueryHandler(IIncomeRepository incomeRepository,
            IIncomeCategoryRepository incomeCategoryRepository,
            IMapper mapper)
        {
            _incomeRepository = incomeRepository;
            _incomeCategoryRepository = incomeCategoryRepository;
            _mapper = mapper;
        }

        public async Task<EditIncomeByIdCommand> Handle(EditIncomeByIdQuery request, CancellationToken cancellationToken)
        {
            var income = await _incomeRepository.GetIncomeById(request.IncomeId);

            var command = _mapper.Map<EditIncomeByIdCommand>(income);

            var categoriesAssignedToUser = await _incomeCategoryRepository
                .GetCategoriesAssignedToLoggedUser();

            var categoryAssignedToUserDtos = _mapper.Map<List<IncomeCategoryAssignedToUserDto>>(categoriesAssignedToUser);

            command.UserCategoryDtos = categoryAssignedToUserDtos;

            return command;
        }
    }
}
