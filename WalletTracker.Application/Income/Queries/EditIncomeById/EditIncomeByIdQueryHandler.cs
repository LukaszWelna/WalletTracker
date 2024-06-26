﻿using AutoMapper;
using MediatR;
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

        // Return EditIncomeByIdCommand object with categories assigned to the logged user
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
