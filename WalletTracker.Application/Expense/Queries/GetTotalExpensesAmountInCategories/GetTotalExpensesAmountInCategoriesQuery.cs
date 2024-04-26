﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Models;

namespace WalletTracker.Application.Expense.Queries.GetTotalAmountInCategories
{
    public class GetTotalExpensesAmountInCategoriesQuery : IRequest<IEnumerable<ExpenseTotalAmountInCategoryDto>>
    {

    }
}