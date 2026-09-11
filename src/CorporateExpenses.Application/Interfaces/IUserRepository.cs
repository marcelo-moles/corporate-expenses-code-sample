using CorporateExpenses.Domain.Entities;

namespace CorporateExpenses.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(
        string username,
        CancellationToken cancellationToken);
}