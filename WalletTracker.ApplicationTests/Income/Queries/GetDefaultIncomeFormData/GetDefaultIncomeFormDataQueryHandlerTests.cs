using AutoMapper;
using FluentAssertions;
using Moq;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Income.Queries.GetCategoriesAssignedToLoggedUser.Tests
{
    public class GetDefaultIncomeFormDataQueryHandlerTests
    {
        [Fact()]
        public async Task Handle_ForGivenId_ReturnCommand()
        {
            // Arrange
            var query = new GetDefaultIncomeFormDataQuery();

            var categoriesAssignedToUser = new List<IncomeCategoryAssignedToUser>()
            {
                new IncomeCategoryAssignedToUser()
                {
                    Id = 1,
                    Name = "Salary"
                }
            };

            var categoryAssignedToUserDtos = new List<IncomeCategoryAssignedToUserDto>()
            {
                new IncomeCategoryAssignedToUserDto()
                {
                    Id = 1,
                    Name = "Salary"
                }
            };

            // Mock Income category repository
            var incomeCategoryRepositoryMock = new Mock<IIncomeCategoryRepository>();

            incomeCategoryRepositoryMock.Setup(i => i.GetCategoriesAssignedToLoggedUser())
                .ReturnsAsync(categoriesAssignedToUser);

            // Mock mapper
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<List<IncomeCategoryAssignedToUserDto>>(categoriesAssignedToUser))
                .Returns(categoryAssignedToUserDtos);

            var handler = new GetDefaultIncomeFormDataQueryHandler(incomeCategoryRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IncomeDate.Should().Be(DateOnly.FromDateTime(DateTime.UtcNow));
            result.UserCategoryDtos.Should().BeEquivalentTo(categoryAssignedToUserDtos);
        }
    }
}