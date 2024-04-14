using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Models;

namespace WalletTracker.Domain.Entities
{
    public class PaymentMethodAssignedToUser
    {
        public int Id { get; set; }
        public string UserId { get; set; } = default!;
        public ApplicationUser User { get; set; } = default!;
        public string Name { get; set; } = default!;
        public List<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
