using CorporateExpenses.Application.DTOs.Expenses;
using CorporateExpenses.Application.Interfaces;
using CorporateExpenses.Domain.Entities;

namespace CorporateExpenses.Application.Managers;

public sealed class ExpenseManager(
    IExpenseRepository repository) : IExpenseManager
{
    public async Task<ExpenseResponse?> GetByIdAsync(
        int id,
        int userId,
        CancellationToken cancellationToken)
    {
        var expense = await repository.GetByIdAsync(
            id,
            userId,
            cancellationToken);

        return expense is null
            ? null
            : Map(expense);
    }

    public async Task<IReadOnlyCollection<ExpenseResponse>> GetByUserAsync(
        int userId,
        CancellationToken cancellationToken)
    {
        var expenses = await repository.GetByUserAsync(
            userId,
            cancellationToken);

        return expenses
            .Select(Map)
            .ToArray();
    }

    public async Task<ExpenseResponse> CreateAsync(
        CreateExpenseRequest request,
        int userId,
        CancellationToken cancellationToken)
    {
        var expense = new Expense(
            request.Amount,
            request.Description,
            userId);

        await repository.AddAsync(
            expense,
            cancellationToken);

        await repository.SaveChangesAsync(
            cancellationToken);

        return Map(expense);
    }

    public async Task<ExpenseResponse?> UpdateAsync(
    int id,
    UpdateExpenseRequest request,
    int userId,
    CancellationToken cancellationToken)
    {
        var expense = await repository.GetByIdAsync(
            id,
            userId,
            cancellationToken);

        if (expense is null)
        {
            return null;
        }

        expense.Update(
            request.Amount,
            request.Description);

        await repository.SaveChangesAsync(
            cancellationToken);

        return Map(expense);
    }

    public async Task<bool> DeleteAsync(
    int id,
    int userId,
    CancellationToken cancellationToken)
    {
        return await repository.DeleteAsync(
            id,
            userId,
            cancellationToken);
    }

    public async Task<ExpenseSummaryResponse> GetSummaryByUserAsync(
    int userId,
    CancellationToken cancellationToken)
    {
        return await repository.GetSummaryByUserAsync(
            userId,
            cancellationToken);
    }

    private static ExpenseResponse Map(Expense expense)
    {
        return new ExpenseResponse(
            expense.Id,
            expense.Amount,
            expense.Description,
            expense.CreatedAt);
    }
}