using AutoMapper;
using FluentAssertions;
using Moq;
using WalletTracker.Application.ApplicationUser;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Settings.Commands.CreateExpenseCategory.Tests
{
    public class CreateExpenseCategoryCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidCommand_ShouldCreateExpenseCategory()
        {
            // Arrange
            var command = new CreateExpenseCategoryCommand()
            {
                Id = 1,
                Name = "Test category",
                Limit = 1000,
                LimitIsActive = true
            };

            var category = new ExpenseCategoryAssignedToUser()
            {
                Id = 1,
                Name = "Test category",
                Limit = 1000,
                LimitIsActive = true,
            };

            var user = new CurrentUser("1", "test@test.com");

            // Mock mapper
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<ExpenseCategoryAssignedToUser>(It.IsAny<CreateExpenseCategoryCommand>()))
                .Returns(category);

            // Mock User context service
            var userContextServiceMock = new Mock<IUserContextService>();

            userContextServiceMock.Setup(u => u.GetCurrentUser())
                .Returns(user);

            // Mock Expense category repository
            var expenseCategoryRepositoryMock = new Mock<IExpenseCategoryRepository>();

            var handler = new CreateExpenseCategoryCommandHandler(expenseCategoryRepositoryMock.Object,
                mapperMock.Object,
                userContextServiceMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            expenseCategoryRepositoryMock.Verify(e => e.Create(category), Times.Once);
            category.UserId.Should().Be(user.Id);
        }
    }
}