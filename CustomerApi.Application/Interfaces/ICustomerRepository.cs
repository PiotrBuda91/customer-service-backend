using CustomerApi.Domain.DTOs;

namespace CustomerApi.Application.Interfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetCustomers();
    Task<Customer> GetCustomerById(string id);
    Task AddCustomer(Customer customer);
    Task UpdateCustomer(Customer customer);
    Task DeleteCustomer(Customer customer);
}
