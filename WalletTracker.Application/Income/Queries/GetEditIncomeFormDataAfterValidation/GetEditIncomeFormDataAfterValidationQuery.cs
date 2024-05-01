using MediatR;
using WalletTracker.Application.Income.Commands.EditIncomeById;

namespace WalletTracker.Application.Income.Queries.GetEditIncomeFormDataAfterValidation
{
    public class GetEditIncomeFormDataAfterValidationQuery : IRequest<EditIncomeByIdCommand>
    {
        public EditIncomeByIdCommand EditIncomeByIdCommand { get; set; }

        public GetEditIncomeFormDataAfterValidationQuery(EditIncomeByIdCommand editIncomeByIdCommand)
        {
            EditIncomeByIdCommand = editIncomeByIdCommand;
        }
    }
}
