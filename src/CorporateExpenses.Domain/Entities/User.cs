namespace CorporateExpenses.Domain.Entities;

public sealed class User
{
    private User()
    {
    }

    public User(
        string username,
        string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException(
                "Username is required.",
                nameof(username));
        }

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            throw new ArgumentException(
                "Password hash is required.",
                nameof(passwordHash));
        }

        Username = username.Trim();
        PasswordHash = passwordHash;
    }

    public int Id { get; private set; }

    public string Username { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;
}