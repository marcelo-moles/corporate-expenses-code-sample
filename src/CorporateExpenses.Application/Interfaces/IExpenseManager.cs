using CorporateExpenses.Application.DTOs.Expenses;

namespace CorporateExpenses.Application.Interfaces;

public interface IExpenseManager
{
    Task<ExpenseResponse?> GetByIdAsync(
        int id,
        int userId,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<ExpenseResponse>> GetByUserAsync(
        int userId,
        CancellationToken cancellationToken);

    Task<ExpenseResponse> CreateAsync(
        CreateExpenseRequest request,
        int userId,
        CancellationToken cancellationToken);

    Task<ExpenseResponse?> UpdateAsync(
    int id,
    UpdateExpenseRequest request,
    int userId,
    CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
    int id,
    int userId,
    CancellationToken cancellationToken);
}