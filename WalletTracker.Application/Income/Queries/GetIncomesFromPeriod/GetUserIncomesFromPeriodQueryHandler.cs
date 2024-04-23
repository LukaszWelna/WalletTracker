using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Income.Queries.GetIncomesFromPeriod
{
    public class GetUserIncomesFromPeriodQueryHandler : IRequestHandler<GetUserIncomesFromPeriodQuery, IEnumerable<IEnumerable<GetIncomeDto>>>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IMapper _mapper;

        public GetUserIncomesFromPeriodQueryHandler(IIncomeRepository incomeRepository, IMapper mapper)
        {
            _incomeRepository = incomeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IEnumerable<GetIncomeDto>>> Handle(GetUserIncomesFromPeriodQuery request, CancellationToken cancellationToken)
        {
            var incomes = await _incomeRepository.GetUserIncomesFromPeriod();

            var incomeDtos = _mapper.Map<List<List<GetIncomeDto>>>(incomes);

            return incomeDtos;
        }
    }
}
