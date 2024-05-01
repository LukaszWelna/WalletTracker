using MediatR;
using WalletTracker.Application.Settings.Commands.EditIncomeCategoryById;

namespace WalletTracker.Application.Settings.Queries.GetIncomeCategoryFormToEdit
{
    public class GetIncomeCategoryFormToEditQuery : IRequest<EditIncomeCategoryByIdCommand>
    {

    }
}
