namespace CorporateExpenses.Application.DTOs.Expenses;

public sealed record ExpenseResponse(
    int Id,
    decimal Amount,
    string Description,
    DateTime CreatedAt);