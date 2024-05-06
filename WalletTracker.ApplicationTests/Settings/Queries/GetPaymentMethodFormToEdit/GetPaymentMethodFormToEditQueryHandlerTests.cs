using Xunit;
using WalletTracker.Application.Settings.Queries.GetPaymentMethodFormToEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using WalletTracker.Application.Expense;
using WalletTracker.Application.Settings.Queries.GetExpenseCategoryFormToEdit;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using FluentAssertions;

namespace WalletTracker.Application.Settings.Queries.GetPaymentMethodFormToEdit.Tests
{
    public class GetPaymentMethodFormToEditQueryHandlerTests
    {
        [Fact()]
        public async Task Handle_ReturnCommand()
        {
            // Arrange
            var query = new GetPaymentMethodFormToEditQuery();

            var paymentMethodsAssignedToUser = new List<PaymentMethodAssignedToUser>()
            {
                new PaymentMethodAssignedToUser()
                {
                    Id = 1,
                    Name = "Cash"
                }
            };

            var paymentMethodAssignedToUserDtos = new List<PaymentMethodAssignedToUserDto>()
            {
                new PaymentMethodAssignedToUserDto()
                {
                    Id = 1,
                    Name = "Cash"
                }
            };

            // Mock Payment method repository
            var paymentMethodRepositoryMock = new Mock<IPaymentMethodRepository>();

            paymentMethodRepositoryMock.Setup(p => p.GetPaymentMethodsAssignedToLoggedUser())
                .ReturnsAsync(paymentMethodsAssignedToUser);

            // Mock mapper
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<List<PaymentMethodAssignedToUserDto>>(paymentMethodsAssignedToUser))
                .Returns(paymentMethodAssignedToUserDtos);

            var handler = new GetPaymentMethodFormToEditQueryHandler(paymentMethodRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.UserPaymentMethodDtos.Should().BeEquivalentTo(paymentMethodAssignedToUserDtos);
        }
    }
}