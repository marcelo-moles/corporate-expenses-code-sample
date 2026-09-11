using System.ComponentModel.DataAnnotations;

namespace CorporateExpenses.Application.DTOs.Auth;

public sealed record LoginRequest(
    [Required]
    [MinLength(3)]
    string Username,

    [Required]
    [MinLength(6)]
    string Password);