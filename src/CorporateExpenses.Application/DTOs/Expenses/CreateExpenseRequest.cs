using System.ComponentModel.DataAnnotations;

namespace CorporateExpenses.Application.DTOs.Expenses;

public sealed record CreateExpenseRequest(
    [Range(0.01, double.MaxValue)]
    decimal Amount,

    [Required]
    [MaxLength(500)]
    string Description);