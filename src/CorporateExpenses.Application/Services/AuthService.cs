using CorporateExpenses.Application.Interfaces;

namespace CorporateExpenses.Application.Services;

public sealed class AuthService(
    IUserRepository userRepository,
    JwtTokenService tokenService) : IAuthService
{
    public async Task<string?> LoginAsync(
     string username,
     string password,
     CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUsernameAsync(
            username,
            cancellationToken);

        if (user is null)
        {
            return null;
        }

        if (!BCrypt.Net.BCrypt.Verify(
                password,
                user.PasswordHash))
        {
            return null;
        }

        return tokenService.GenerateToken(user.Id);
    }
}