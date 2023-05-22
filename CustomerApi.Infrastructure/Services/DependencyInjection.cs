using CustomerApi.Application.Interfaces;
using CustomerApi.Infrastructure.Data;
using CustomerApi.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerApi.Infrastructure.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddDbContext<CustomerContext>();

        return services;
    }
}