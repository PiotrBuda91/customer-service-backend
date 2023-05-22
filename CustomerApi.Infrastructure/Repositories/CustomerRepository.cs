using CustomerApi.Domain.DTOs;
using CustomerApi.Application.Interfaces;
using CustomerApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerContext _context;

    public CustomerRepository(CustomerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context), "My context cannot be null."); ;
    }

    public async Task<IEnumerable<Customer>> GetCustomers()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer> GetCustomerById(string id)
    {
        return await _context.Customers.FindAsync(id) ?? throw new ArgumentNullException(nameof(id), "id not found.");
    }

    public async Task AddCustomer(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCustomer(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCustomer(Customer customer)
    {

        var foundCustomer = await _context.Customers.FindAsync(customer.Id);
        if (foundCustomer != null)
        {
            _context.Customers.Remove(foundCustomer);
            await _context.SaveChangesAsync();
        }
  
    }
}