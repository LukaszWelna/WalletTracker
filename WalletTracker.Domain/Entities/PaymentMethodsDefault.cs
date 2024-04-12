using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTracker.Domain.Entities
{
    public class PaymentMethodsDefault
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
