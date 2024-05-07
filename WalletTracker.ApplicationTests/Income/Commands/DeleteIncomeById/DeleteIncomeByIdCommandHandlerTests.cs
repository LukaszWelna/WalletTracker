using Xunit;
using WalletTracker.Application.Income.Commands.DeleteIncomeById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WalletTracker.Domain.Interfaces;
using WalletTracker.Application.Income.Commands.CreateIncome;
using FluentAssertions;

namespace WalletTracker.Application.Income.Commands.DeleteIncomeById.Tests
{
    public class DeleteIncomeByIdCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidId_ShouldDeleteIncome()
        {
            // Arrange
            var command = new DeleteIncomeByIdCommand(1);

            // Mock Income repository
            var incomeRepositoryMock = new Mock<IIncomeRepository>();

            var handler = new DeleteIncomeByIdCommandHandler(incomeRepositoryMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            incomeRepositoryMock.Verify(i => i.DeleteIncomeById(command.IncomeId), Times.Once);
        }

        [Fact()]
        public void Handle_WithInvalidId_ShouldThrowException()
        {
            // Arrange
            var command = new DeleteIncomeByIdCommand(0);

            // Mock Income repository
            var incomeRepositoryMock = new Mock<IIncomeRepository>();

            var handler = new DeleteIncomeByIdCommandHandler(incomeRepositoryMock.Object);

            // Act
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            func.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}