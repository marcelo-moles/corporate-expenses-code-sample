namespace CorporateExpenses.Domain.Entities;

public sealed class Expense
{
    private Expense()
    {
    }

    public Expense(
        decimal amount,
        string description,
        int userId)
    {
        if (amount <= 0)
        {
            throw new ArgumentException(
                "Expense amount must be greater than zero.",
                nameof(amount));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException(
                "Expense description is required.",
                nameof(description));
        }

        if (userId <= 0)
        {
            throw new ArgumentException(
                "UserId must be greater than zero.",
                nameof(userId));
        }

        Amount = amount;
        Description = description.Trim();
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(
    decimal amount,
    string description)
    {
        if (amount <= 0)
        {
            throw new ArgumentException(
                "Expense amount must be greater than zero.",
                nameof(amount));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException(
                "Expense description is required.",
                nameof(description));
        }

        Amount = amount;
        Description = description.Trim();
    }

    public int Id { get; private set; }

    public decimal Amount { get; private set; }

    public string Description { get; private set; } = string.Empty;

    public int UserId { get; private set; }

    public DateTime CreatedAt { get; private set; }
}