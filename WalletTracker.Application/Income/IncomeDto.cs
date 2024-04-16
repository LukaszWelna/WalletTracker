using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Entities;

namespace WalletTracker.Application.Income
{
    public class IncomeDto
    {
        public decimal Amount { get; set; }
        public DateOnly IncomeDate { get; set; }
        public int CategoryId { get; set; }
        public string? Comment { get; set; }
    }
}
