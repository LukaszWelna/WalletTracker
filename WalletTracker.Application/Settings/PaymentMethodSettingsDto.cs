﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Expense;
using WalletTracker.Application.Income;
using WalletTracker.Domain.Entities;

namespace WalletTracker.Application.Settings
{
    public class PaymentMethodSettingsDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<PaymentMethodAssignedToUserDto> UserPaymentMethodDtos { get; set; } = new List<PaymentMethodAssignedToUserDto>();
    }
}
