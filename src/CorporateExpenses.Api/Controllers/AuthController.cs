using CorporateExpenses.Application.DTOs.Auth;
using CorporateExpenses.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CorporateExpenses.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController(
    IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(
        typeof(LoginResponse),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResponse>> Login(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        var token = await authService.LoginAsync(
            request.Username,
            request.Password,
            cancellationToken);

        if (token is null)
        {
            return Unauthorized();
        }

        return Ok(new LoginResponse(token));
    }
}