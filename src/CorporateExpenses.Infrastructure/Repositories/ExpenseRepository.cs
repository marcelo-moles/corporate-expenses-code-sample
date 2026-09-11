using CorporateExpenses.Application.Interfaces;
using CorporateExpenses.Domain.Entities;
using CorporateExpenses.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CorporateExpenses.Infrastructure.Repositories;

public sealed class ExpenseRepository(
    ExpensesDbContext context) : IExpenseRepository
{
    public async Task<Expense?> GetByIdAsync(
        int id,
        int userId,
        CancellationToken cancellationToken)
    {
        return await context.Expenses
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == id &&
                     x.UserId == userId,
                cancellationToken);
    }

    public async Task<IReadOnlyCollection<Expense>> GetByUserAsync(
        int userId,
        CancellationToken cancellationToken)
    {
        return await context.Expenses
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        Expense expense,
        CancellationToken cancellationToken)
    {
        await context.Expenses.AddAsync(
            expense,
            cancellationToken);
    }

    public Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        return context.SaveChangesAsync(
            cancellationToken);
    }
}