namespace CorporateExpenses.Application.Interfaces;

public interface IAuthService
{
    Task<string?> LoginAsync(
        string username,
        string password,
        CancellationToken cancellationToken);
}