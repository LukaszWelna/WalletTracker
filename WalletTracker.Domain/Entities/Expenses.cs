﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTracker.Domain.Entities
{
    public class Expenses
    {
        public int Id { get; set; }
        public string UserId { get; set; } = default!;
        public IdentityUser User { get; set; } = default!;
        public int CategoryId { get; set; }
        public ExpenseCategoriesAssignedToUsers Category { get; set; } = default!;
        public int PaymentId { get; set; }
        public PaymentMethodsAssignedToUsers Payment { get; set; } = default!;
        public decimal Amount { get; set; }
        public DateOnly ExpenseDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? Comment { get; set; }
    }
}
