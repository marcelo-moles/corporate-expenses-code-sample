namespace CorporateExpenses.Application.DTOs.Expenses;

public sealed record CreateExpenseRequest(
    decimal Amount,
    string Description);