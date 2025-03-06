using IntercityTaxi.API.Contracts.User;
using IntercityTaxi.API.Controllers;
using IntercityTaxi.Application.Abstractions;
using IntercityTaxi.Domain.Interfaces;
using IntercityTaxi.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class UserControllerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _controller = new UserController(_mockUserService.Object);
    }

    [Fact]
    public async Task RegisterClient_ShouldReturnBadRequest_WhenPasswordIsEmpty()
    {
        // Arrange
        var request = new RegisterUser("71234567890", "", ""); // Пустой пароль

        // Act
        var result = await _controller.RegisterClient(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Пароль не должен быть пустым./Password must not be empty.",
                     ((dynamic)badRequestResult.Value).message);
    }

    [Fact]
    public async Task RegisterClient_ShouldReturnBadRequest_WhenPasswordsDoNotMatch()
    {
        // Arrange
        var request = new RegisterUser("71234567890", "password123", "differentPassword");

        // Act
        var result = await _controller.RegisterClient(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Пароли не совпадают./Passwords do not match.",
                     ((dynamic)badRequestResult.Value).message);
    }

    [Fact]
    public async Task RegisterClient_ShouldReturnBadRequest_WhenPhoneNumberIsInvalid()
    {
        // Arrange
        var request = new RegisterUser("123", "password123", "password123");

        _mockUserService
            .Setup(s => s.RegisterClientAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<Client>.Failure("Invalid phone number format."));

        // Act
        var result = await _controller.RegisterClient(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid phone number format.", badRequestResult.Value);
    }

    [Fact]
    public async Task RegisterClient_ShouldReturnOk_WhenRegistrationIsSuccessful()
    {
        // Arrange
        //var request = new RegisterUser("71234567890", "password123", "password123");
        //var fakeClient = new Client { PhoneNumber = request.PhoneNumber };

        //_mockUserService
        //    .Setup(s => s.RegisterClientAsync(request.PhoneNumber, request.PasswordBase))
        //    .ReturnsAsync(Result<Client>.Success(fakeClient));

        //// Act
        //var result = await _controller.RegisterClient(request);

        //// Assert
        //var okResult = Assert.IsType<OkObjectResult>(result);
        //var returnedClient = Assert.IsType<Client>(okResult.Value);
        //Assert.Equal(request.PhoneNumber, returnedClient.PhoneNumber);
    }
}
