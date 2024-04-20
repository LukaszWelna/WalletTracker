using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Entities;

namespace WalletTracker.Application.Income
{
    public class IncomeDto
    {
        public decimal Amount { get; set; }
        public DateOnly IncomeDate { get; set; }
        public int CategoryId { get; set; }
        public List<IncomeCategoryAssignedToUserDto> UserCategoryDtos { get; set; } = new List<IncomeCategoryAssignedToUserDto>();
        public string? Comment { get; set; }
    }
}
