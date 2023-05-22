using CustomerApi.Domain.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Infrastructure.Data;

public class CustomerContext : DbContext
{
    public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
}