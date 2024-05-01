using MediatR;
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
