using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTracker.Domain.Models
{
    public class IncomeTotalAmountInCategoryDto
    {
        public string CategoryName { get; set; } = default!;
        public decimal TotalAmount { get; set; }
    }
}
