using IntercityTaxi.API.Contracts.User;
using IntercityTaxi.Application.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntercityTaxi.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(
    IUserService userService
        ) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("register")]
    public async Task<IActionResult> RegisterClient([FromBody] RegisterUser requestUser)
    {
        if (!requestUser.AreEmptyPassword())
        {
            return BadRequest(new { message = "Пароль не должен быть пустым./Password must not be empty." });
        }

        if (!requestUser.ArePasswordsMatching())
        {
            return BadRequest(new { message = "Пароли не совпадают./Passwords do not match." });
        }

        var validPhoneNumberResult = PhoneNumberValidator.ConvertToStandardFormat(requestUser.PhoneNumber);
        if (!validPhoneNumberResult.IsSuccess)
        {
            return BadRequest(validPhoneNumberResult.Error);
        }

        var result = await _userService.RegisterClientAsync(validPhoneNumberResult.Value, requestUser.PasswordBase);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
        
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUser loginUser)
    {
        if (!loginUser.AreEmptyPassword())
            return BadRequest(new { message = "Пароль не должен быть пустым./Password must not be empty." });

        var validPhoneNumberResult = PhoneNumberValidator.ConvertToStandardFormat(loginUser.PhoneNumber);
        if (!validPhoneNumberResult.IsSuccess)
        {
            return BadRequest(validPhoneNumberResult.Error);
        }


        var resultLogin = await _userService.Login(validPhoneNumberResult.Value, loginUser.Password);
        return resultLogin.IsSuccess ?
                Ok(resultLogin.Value) :
                Unauthorized(resultLogin.Error);
    }

    [HttpPost("forgotpassword")]
    public async Task<ActionResult> ForgotPassword([FromBody] ForgotPassword forgotPassword)
    {
        if (!forgotPassword.AreEmptyPassword())
            return BadRequest(new { message = "Пароль не должен быть пустым./Password must not be empty." });

        if (!forgotPassword.ArePasswordsMatching())
            return BadRequest(new { message = "Пароли не совпадают./Passwords do not match." });
        
        var validPhoneNumberResult = PhoneNumberValidator.ConvertToStandardFormat(forgotPassword.PhoneNumber);
        if (!validPhoneNumberResult.IsSuccess)
        {
            return BadRequest(validPhoneNumberResult.Error);
        }
        
        var resultForgot = await _userService.ChangePasswordAsync(validPhoneNumberResult.Value, forgotPassword.PasswordBase);
        return resultForgot.IsSuccess ?
                Ok(resultForgot.Value) :
                BadRequest(resultForgot.Error);

    }

    [Authorize]
    [HttpPost("refreshtoken")]
    public async Task<ActionResult> RefreshToken(RefreshTokenModel refreshTokenModel)
    {
        if (!refreshTokenModel.AreEmptyTokens())
            return BadRequest(new { message = "Токен не должен быть пустым./Token must not be empty." });

        var refreshTokenResult = await _userService.RefreshToken(refreshTokenModel.AccessToken, refreshTokenModel.RefreshToken);
        return refreshTokenResult.IsSuccess ?
                Ok(refreshTokenResult.Value) :
                Unauthorized(refreshTokenResult.Error);
    }

    [HttpGet("users")]
    public async Task<ActionResult> GetAllUsers()
    {
        var getUsersResult = await _userService.GetAllUsers();

        if (getUsersResult.IsSuccess)
        {
            return Ok(getUsersResult.Value);
        }
        else
        {
            return BadRequest(getUsersResult.Error);
        }

    }
}
