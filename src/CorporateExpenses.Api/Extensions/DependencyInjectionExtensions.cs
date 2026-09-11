using CorporateExpenses.Application.Interfaces;
using CorporateExpenses.Application.Managers;
using CorporateExpenses.Application.Services;
using CorporateExpenses.Infrastructure.Data;
using CorporateExpenses.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CorporateExpenses.Api.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IExpenseManager, ExpenseManager>();

        services.AddScoped<JwtTokenService>();

        services.AddScoped<IAuthService, AuthService>();

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ExpensesDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IExpenseRepository, ExpenseRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}