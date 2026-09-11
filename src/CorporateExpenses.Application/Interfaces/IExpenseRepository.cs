using CorporateExpenses.Domain.Entities;

namespace CorporateExpenses.Application.Interfaces;

public interface IExpenseRepository
{
    Task<Expense?> GetByIdAsync(
        int id,
        int userId,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Expense>> GetByUserAsync(
        int userId,
        CancellationToken cancellationToken);

    Task AddAsync(
        Expense expense,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}