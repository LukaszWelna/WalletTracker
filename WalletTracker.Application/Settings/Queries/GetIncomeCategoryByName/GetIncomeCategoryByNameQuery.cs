using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Entities;

namespace WalletTracker.Application.Settings.Queries.GetIncomeCategoryByName
{
    public class GetIncomeCategoryByNameQuery : IRequest<IncomeCategoryAssignedToUser?>
    {
        public string Name { get; set; }
        public GetIncomeCategoryByNameQuery(string name)
        {
            Name = name;
        }
    }
}
