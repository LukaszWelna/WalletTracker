using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTracker.Application.Balance.Queries.GetBalanceData
{
    public class GetBalanceDataQueryHandler : IRequestHandler<GetBalanceDataQuery, IEnumerable<BalanceDto>>
    {
        private readonly IMapper _mapper;

        public GetBalanceDataQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<IEnumerable<BalanceDto>> Handle(GetBalanceDataQuery request, CancellationToken cancellationToken)
        {
            var balanceDtos = _mapper.Map<IEnumerable<BalanceDto>>(request.ExpenseTotalAmountInCategories);

            return Task.FromResult(balanceDtos);
        }
    }
}
