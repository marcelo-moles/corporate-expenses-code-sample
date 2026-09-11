namespace CorporateExpenses.Application.DTOs.Expenses;

public sealed record ExpenseSummaryResponse(
    decimal TotalAmount,
    int TotalExpenses);
