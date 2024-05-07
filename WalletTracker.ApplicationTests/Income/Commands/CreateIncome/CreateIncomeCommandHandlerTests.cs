using Xunit;
using WalletTracker.Application.Income.Commands.CreateIncome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WalletTracker.Application.ApplicationUser;
using AutoMapper;
using WalletTracker.Domain.Interfaces;
using FluentAssertions;

namespace WalletTracker.Application.Income.Commands.CreateIncome.Tests
{
    public class CreateIncomeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidCommand_ShouldCreateIncome()
        {
            // Arrange
            var command = new CreateIncomeCommand()
            {
                Id = 1,
                CategoryId = 1,
                Amount = 100,
                IncomeDate = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            var income = new Domain.Entities.Income()
            {
                Id = 1,
                CategoryId = 1,
                Amount = 100,
                IncomeDate = DateOnly.FromDateTime(DateTime.UtcNow),
                CreatedAt = DateTime.UtcNow
            };

            var user = new CurrentUser("1", "test@test.com");

            // Mock mapper
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<Domain.Entities.Income>(It.IsAny<CreateIncomeCommand>()))
                .Returns(income);

            // Mock User context service
            var userContextServiceMock = new Mock<IUserContextService>();

            userContextServiceMock.Setup(u => u.GetCurrentUser())
                .Returns(user);

            // Mock Income repository
            var incomeRepositoryMock = new Mock<IIncomeRepository>();

            // Create handler
            var handler = new CreateIncomeCommandHandler(incomeRepositoryMock.Object, mapperMock.Object, userContextServiceMock.Object);

            // Act 
            await handler.Handle(command, CancellationToken.None);

            // Assert
            incomeRepositoryMock.Verify(i => i.Create(income), Times.Once);
            income.UserId.Should().Be(user.Id);
        }
    }
}