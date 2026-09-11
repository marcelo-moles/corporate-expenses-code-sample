using CorporateExpenses.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CorporateExpenses.Infrastructure.Data;

public sealed class ExpensesDbContext(
    DbContextOptions<ExpensesDbContext> options)
    : DbContext(options)
{
    public DbSet<Expense> Expenses => Set<Expense>();

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>(entity =>
        {
            entity.ToTable("Expenses");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(x => x.Description)
                .HasMaxLength(500)
                .IsRequired();

            entity.Property(x => x.UserId)
                .IsRequired();

            entity.Property(x => x.CreatedAt)
                .IsRequired();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Username)
                .HasMaxLength(100)
                .IsRequired();

            entity.HasIndex(x => x.Username)
                .IsUnique();

            entity.Property(x => x.PasswordHash)
                .HasMaxLength(500)
                .IsRequired();
        });
    }
}