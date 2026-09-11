using CorporateExpenses.Application.DTOs.Expenses;
using CorporateExpenses.Application.Interfaces;
using CorporateExpenses.Domain.Entities;
using CorporateExpenses.Infrastructure.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

    public async Task<bool> DeleteAsync(
    int id,
    int userId,
    CancellationToken cancellationToken)
    {
        var expense = await context.Expenses
            .FirstOrDefaultAsync(
                x => x.Id == id &&
                     x.UserId == userId,
                cancellationToken);

        if (expense is null)
        {
            return false;
        }

        context.Expenses.Remove(expense);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        return context.SaveChangesAsync(
            cancellationToken);
    }

    public async Task<ExpenseSummaryResponse> GetSummaryByUserAsync(
    int userId,
    CancellationToken cancellationToken)
    {
        const string sql = """
        SELECT
            ISNULL(SUM(Amount), 0) AS TotalAmount,
            COUNT(*) AS TotalExpenses
        FROM Expenses
        WHERE UserId = @UserId;
        """;

        using var connection = context.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync(cancellationToken);
        }

        var command = new CommandDefinition(
            sql,
            new { UserId = userId },
            cancellationToken: cancellationToken);

        return await connection.QuerySingleAsync<ExpenseSummaryResponse>(
            command);
    }
}