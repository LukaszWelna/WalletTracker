using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTracker.Application.Expense
{
    public class CategoryLimitDto
    {
        public decimal? Limit { get; set; }
        public bool LimitIsActive { get; set; }
    }
}
