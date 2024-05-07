using Xunit;
using WalletTracker.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WalletTracker.Application.Expense;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Models;
using WalletTracker.Application.Balance;
using FluentAssertions;

namespace WalletTracker.Application.Mappings.Tests
{
    public class BalanceMappingProfileTests
    {
        [Fact]
        public void BalanceMappingProfile_ShouldMapExpenseTotalAmountInCategoryDtoToBalanceCanvasDto()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<BalanceMappingProfile>());

            var mapper = configuration.CreateMapper();

            var expenseTotalAmountInCategoryDto = new ExpenseTotalAmountInCategoryDto()
            {
                CategoryName = "TestCategoryName",
                TotalAmount = 100
            };

            // Act
            var result = mapper.Map<BalanceCanvasDto>(expenseTotalAmountInCategoryDto);

            // Assert
            result.Should().NotBeNull();
            result.label.Should().Be(expenseTotalAmountInCategoryDto.CategoryName);
            result.y.Should().Be(expenseTotalAmountInCategoryDto.TotalAmount);
        }
    }
}