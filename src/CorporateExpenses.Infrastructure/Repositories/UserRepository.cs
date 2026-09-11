using CorporateExpenses.Application.Interfaces;
using CorporateExpenses.Domain.Entities;
using CorporateExpenses.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CorporateExpenses.Infrastructure.Repositories;

public sealed class UserRepository(
    ExpensesDbContext context) : IUserRepository
{
    public async Task<User?> GetByUsernameAsync(
        string username,
        CancellationToken cancellationToken)
    {
        return await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Username == username,
                cancellationToken);
    }
}