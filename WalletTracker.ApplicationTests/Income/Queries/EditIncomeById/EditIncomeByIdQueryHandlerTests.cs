using Xunit;
using WalletTracker.Application.Income.Queries.EditIncomeById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Income.Commands.EditIncomeById;
using WalletTracker.Domain.Entities;
using Moq;
using WalletTracker.Domain.Interfaces;
using AutoMapper;
using FluentAssertions;

namespace WalletTracker.Application.Income.Queries.EditIncomeById.Tests
{
    public class EditIncomeByIdQueryHandlerTests
    {
        [Fact()]
        public async Task Handle_ForGivenId_ReturnCommand()
        {
            // Arrange 
            var query = new EditIncomeByIdQuery(1);

            var income = new Domain.Entities.Income()
            {
                Id = 1,
                CategoryId = 1,
                Amount = 100,
                IncomeDate = DateOnly.FromDateTime(DateTime.UtcNow),
                CreatedAt = DateTime.UtcNow,
                Comment = "Comment"
            };

            var command = new EditIncomeByIdCommand()
            {
                Id = 1,
                CategoryId = 1,
                Amount = 100,
                IncomeDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Comment = "Comment"
            };

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

            // Mock Income repository
            var incomeRepositoryMock = new Mock<IIncomeRepository>();

            incomeRepositoryMock.Setup(i => i.GetIncomeById(query.IncomeId))
                .ReturnsAsync(income);

            // Mock Income category repository
            var incomeCategoryRepositoryMock = new Mock<IIncomeCategoryRepository>();

            incomeCategoryRepositoryMock.Setup(i => i.GetCategoriesAssignedToLoggedUser())
                .ReturnsAsync(categoriesAssignedToUser);

            // Mock mapper
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<EditIncomeByIdCommand>(income))
                .Returns(command);

            mapperMock.Setup(m => m.Map<List<IncomeCategoryAssignedToUserDto>>(categoriesAssignedToUser))
                .Returns(categoryAssignedToUserDtos);

            var handler = new EditIncomeByIdQueryHandler(incomeRepositoryMock.Object, 
                incomeCategoryRepositoryMock.Object,
                mapperMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.UserCategoryDtos.Should().BeEquivalentTo(categoryAssignedToUserDtos);
        }
    }
}