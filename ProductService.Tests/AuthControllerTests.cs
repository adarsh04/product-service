using Moq;
using System.Text;
using Xunit;
using ProductService.Controllers;
using ProductService.Models;
using ProductService.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

public class AuthControllerTests
{

    private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
    private readonly Mock<IOptions<JwtSettings>> _mockOptions;
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();
        _mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

        // Mock the IOptions wrapper
        _mockOptions = new Mock<IOptions<JwtSettings>>();
        _mockOptions.Setup(o => o.Value).Returns(new JwtSettings 
        { 
            Key = "SuperSecretTestingKeyThatIsLongEnough123!", 
            Issuer = "TestIssuer", 
            Audience = "TestAudience",
            DurationInMinutes = 60
        });

        _controller = new AuthController(_mockUserManager.Object, _mockOptions.Object);
    }

    [Fact]
    public async Task Login_ReturnsOk_WhenCreditialsAreValid()
    {
        var loginRequest = new LoginRequest { Email = "test@test.com", Password = "Password123!" };
        var user = new ApplicationUser { Email = "test@test.com", UserName = "testuser" };

        _mockUserManager.Setup(x => x.FindByEmailAsync(loginRequest.Email))
            .ReturnsAsync(user);
        _mockUserManager.Setup(x => x.CheckPasswordAsync(user, loginRequest.Password))
            .ReturnsAsync(true);

        var result = await _controller.Login(loginRequest);

        Assert.IsType<OkObjectResult>(result);
    }


    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenPasswordIsWrong()
    {
        var loginRequest = new LoginRequest { Email = "test@test.com", Password = "WrongPassword" };
        var user = new ApplicationUser { Email = "test@test.com", UserName = "testuser" };

        _mockUserManager.Setup(x => x.FindByEmailAsync(loginRequest.Email))
            .ReturnsAsync(new ApplicationUser());
        _mockUserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<ApplicationUser>(), loginRequest.Password))
            .ReturnsAsync(false);

        var result = await _controller.Login(loginRequest);

        Assert.IsType<UnauthorizedObjectResult>(result);
    }
}