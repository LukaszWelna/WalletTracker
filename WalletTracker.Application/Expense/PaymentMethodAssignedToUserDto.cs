﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTracker.Application.Expense
{
    public class PaymentMethodAssignedToUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
    }
}