using FluentAssertions;
using Moq;
using WalletTracker.Domain.Interfaces;
using Xunit;

namespace WalletTracker.Application.Income.Commands.EditIncomeById.Tests
{
    public class EditIncomeByIdCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidCommand_ShouldEditIncomeById()
        {
            // Arrange
            var command = new EditIncomeByIdCommand()
            {
                Id = 1,
                CategoryId = 2,
                Amount = 1000,
                IncomeDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-1),
                Comment = "New comment"
            };

            var income = new Domain.Entities.Income()
            {
                Id = 1,
                CategoryId = 1,
                Amount = 100,
                IncomeDate = DateOnly.FromDateTime(DateTime.UtcNow),
                CreatedAt = DateTime.UtcNow,
                Comment = "Comment"
            };

            // Mock Income repository
            var incomeRepositoryMock = new Mock<IIncomeRepository>();

            incomeRepositoryMock.Setup(i => i.GetIncomeById(It.IsAny<int>()))
                .ReturnsAsync(income);

            var handler = new EditIncomeByIdCommandHandler(incomeRepositoryMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            incomeRepositoryMock.Verify(i => i.Commit(), Times.Once);
            income.CategoryId.Should().Be(command.CategoryId);
            income.Amount.Should().Be(command.Amount);
            income.IncomeDate.Should().Be(command.IncomeDate);
            income.Comment.Should().Be(command.Comment);
        }
    }
}