using AutoMapper;
using FluentAssertions;
using Moq;
using WalletTracker.Application.Income.Commands.EditIncomeById;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Income.Queries.GetEditIncomeFormDataAfterValidation.Tests
{
    public class GetEditIncomeFormDataAfterValidationQueryHandlerTests
    {
        [Fact()]
        public async void Handle_ForGivenCommand_ReturnUpdatedCommand()
        {
            // Arrange
            var command = new EditIncomeByIdCommand()
            {
                Id = 1,
                CategoryId = 1,
                Amount = 100,
                IncomeDate = DateOnly.FromDateTime(DateTime.UtcNow),
                UserCategoryDtos = new List<IncomeCategoryAssignedToUserDto>(),
                Comment = "Comment"
            };

            var query = new GetEditIncomeFormDataAfterValidationQuery(command);

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

            var handler = new GetEditIncomeFormDataAfterValidationQueryHandler(incomeCategoryRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.UserCategoryDtos.Should().BeEquivalentTo(categoryAssignedToUserDtos);
        }
    }
}