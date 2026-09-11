using CorporateExpenses.Application.DTOs.Expenses;
using CorporateExpenses.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace CorporateExpenses.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public sealed class ExpensesController(
    IExpenseManager manager) : ControllerBase
{
    [HttpGet("{id:int}")]
    [ProducesResponseType(
        typeof(ExpenseResponse),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExpenseResponse>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();

        var expense = await manager.GetByIdAsync(
            id,
            userId,
            cancellationToken);

        return expense is null
            ? NotFound()
            : Ok(expense);
    }

    [HttpGet]
    [ProducesResponseType(
        typeof(IReadOnlyCollection<ExpenseResponse>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<ExpenseResponse>>> Get(
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();

        var expenses = await manager.GetByUserAsync(
            userId,
            cancellationToken);

        return Ok(expenses);
    }

    [HttpPost]
    [ProducesResponseType(
        typeof(ExpenseResponse),
        StatusCodes.Status201Created)]
    public async Task<ActionResult<ExpenseResponse>> Create(
        CreateExpenseRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();

        var expense = await manager.CreateAsync(
            request,
            userId,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = expense.Id },
            expense);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(
    typeof(ExpenseResponse),
    StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExpenseResponse>> Update(
    int id,
    UpdateExpenseRequest request,
    CancellationToken cancellationToken)
    {
        var userId = GetUserId();

        var expense = await manager.UpdateAsync(
            id,
            request,
            userId,
            cancellationToken);

        return expense is null
            ? NotFound()
            : Ok(expense);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
    int id,
    CancellationToken cancellationToken)
    {
        var userId = GetUserId();

        var deleted = await manager.DeleteAsync(
            id,
            userId,
            cancellationToken);

        return deleted
            ? NoContent()
            : NotFound();
    }

    [HttpGet("summary")]
    [ProducesResponseType(
    typeof(ExpenseSummaryResponse),
    StatusCodes.Status200OK)]
    public async Task<ActionResult<ExpenseSummaryResponse>> GetSummary(
    CancellationToken cancellationToken)
    {
        var userId = GetUserId();

        var summary = await manager.GetSummaryByUserAsync(
            userId,
            cancellationToken);

        return Ok(summary);
    }

    private int GetUserId()
    {
        var userIdClaim = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out var userId))
        {
            throw new UnauthorizedAccessException(
                "Authenticated user ID is missing.");
        }

        return userId;
    }
}