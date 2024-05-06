using AutoMapper;
using FluentAssertions;
using Moq;
using WalletTracker.Application.ApplicationUser;
using WalletTracker.Application.Settings.Commands.CreateIncomeCategory;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Settings.Commands.CreateCategory.Tests
{
    public class CreateIncomeCategoryCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_WithValidCommand_CreateIncomeCategory()
        {
            // Arrange
            var command = new CreateIncomeCategoryCommand()
            {
                Id = 1,
                Name = "Test category"
            };

            var category = new IncomeCategoryAssignedToUser()
            {
                Id = 1,
                Name = "Test category"
            };

            var user = new CurrentUser("1", "test@test.com");

            // Mock mapper
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<IncomeCategoryAssignedToUser>(It.IsAny<CreateIncomeCategoryCommand>()))
                .Returns(category);

            // Mock User context service
            var userContextServiceMock = new Mock<IUserContextService>();

            userContextServiceMock.Setup(u => u.GetCurrentUser())
                .Returns(user);

            // Mock Income category repository
            var incomeCategoryRepositoryMock = new Mock<IIncomeCategoryRepository>();

            var handler = new CreateIncomeCategoryCommandHandler(incomeCategoryRepositoryMock.Object,
                userContextServiceMock.Object,
                mapperMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            incomeCategoryRepositoryMock.Verify(i => i.Create(category), Times.Once);
            category.UserId.Should().Be(user.Id);
        }
    }
}