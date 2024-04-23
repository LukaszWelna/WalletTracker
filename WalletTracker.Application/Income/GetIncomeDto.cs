using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTracker.Application.Income
{
    public class GetIncomeDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateOnly IncomeDate { get; set; }
        public string CategoryName { get; set; } = default!;
        public string? Comment { get; set; }
    }
}
