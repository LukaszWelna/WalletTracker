﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Entities;

namespace WalletTracker.Domain.Interfaces
{
    public interface IIncomeRepository
    {
        public Task Create(Income income);
        public Task<IEnumerable<IncomeCategoryDefault>> GetDefaultCategories();
        public Task SeedDefaultCategoriesToUser(IEnumerable<IncomeCategoryAssignedToUser> incomeCategoriesAssignedToUser);
        public Task<IEnumerable<IncomeCategoryAssignedToUser>> GetCategoriesAssignedToLoggedUser();
        public Task<IEnumerable<IEnumerable<Income>>> GetUserIncomesFromPeriod();
    }
}
