﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTracker.Domain.Entities
{
    public class PaymentMethodsAssignedToUsers
    {
        public int Id { get; set; }
        public string UserId { get; set; } = default!;
        public IdentityUser User { get; set; } = default!;
        public string Name { get; set; } = default!;
        public List<Expenses> Expenses { get; set; } = new List<Expenses>();
    }
}
