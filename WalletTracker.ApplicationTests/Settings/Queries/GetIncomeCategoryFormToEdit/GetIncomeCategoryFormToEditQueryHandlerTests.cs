using AutoMapper;
using FluentAssertions;
using Moq;
using WalletTracker.Application.Income;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Settings.Queries.GetIncomeCategoryFormToEdit.Tests
{
    public class GetIncomeCategoryFormToEditQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnCommand()
        {
            // Arrange
            var query = new GetIncomeCategoryFormToEditQuery();

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

            var handler = new GetIncomeCategoryFormToEditQueryHandler(incomeCategoryRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.UserCategoryDtos.Should().BeEquivalentTo(categoryAssignedToUserDtos);
        }
    }
}