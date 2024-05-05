using AutoMapper;
using FluentAssertions;
using WalletTracker.Application.Income;
using WalletTracker.Domain.Entities;
using Xunit;

namespace WalletTracker.Application.Mappings.Tests
{
    public class IncomeMappingProfileTests
    {
        [Fact()]
        public void IncomeMappingProfile_ShouldMapIncomeToGetIncomeDto()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<IncomeMappingProfile>());

            var mapper = configuration.CreateMapper();

            var income = new Domain.Entities.Income()
            {
                Category = new IncomeCategoryAssignedToUser()
                {
                    Name = "TestName"
                }
            };

            // Act
            var result = mapper.Map<GetIncomeDto>(income);

            // Assert
            result.Should().NotBeNull();
            result.CategoryName.Should().Be(income.Category.Name);
        }
    }
}