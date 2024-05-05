using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using Xunit;

namespace WalletTracker.Application.ApplicationUser.Tests
{
    public class UserContextServiceTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Email, "test@test.com")
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });

            var userContext = new UserContextService(httpContextAccessorMock.Object);

            // Act
            var currentUser = userContext.GetCurrentUser();

            // Assert
            currentUser.Should().NotBeNull();
            currentUser.Id.Should().Be("1");
            currentUser.Email.Should().Be("test@test.com");
        }

        [Fact()]
        public void GetCurrentUser_WithEmptyUser_ShouldThrowException()
        {
            // Arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {

            });

            var userContext = new UserContextService(httpContextAccessorMock.Object);

            // Act
            Action action = () => userContext.GetCurrentUser();

            // Assert
            action.Invoking(a => a.Invoke())
                .Should().Throw<InvalidOperationException>();
        }

        [Fact()]
        public void GetCurrentUser_WithoutIdentity_ShouldThrowException()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Email, "test@test.com")
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims));

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });

            var userContext = new UserContextService(httpContextAccessorMock.Object);

            // Act
            Action action = () => userContext.GetCurrentUser();

            // Assert
            action.Invoking(a => a.Invoke())
                .Should().Throw<InvalidOperationException>();
        }
    }
}