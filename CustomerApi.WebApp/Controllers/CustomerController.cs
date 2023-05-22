using CustomerApi.Domain.DTOs;
using CustomerApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository)); ;
        }

        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _customerRepository.GetCustomers();
        }

        /// <summary>
        /// Retrieves a customer by ID.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(string id)
        {
            var customer = await _customerRepository.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="customer">The customer to create.</param>
        [HttpPost]
        public async Task<ActionResult> CreateCustomer(Customer customer)
        {
            await _customerRepository.AddCustomer(customer);

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="id">The ID of the customer to update.</param>
        /// <param name="customer">The updated customer data.</param>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(string id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

           await _customerRepository.UpdateCustomer(customer);

            return NoContent();
        }

        /// <summary>
        /// Deletes a customer by ID.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(string id)
        {
            var customer = await _customerRepository.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }

            await  _customerRepository.DeleteCustomer(customer);

            return NoContent();
        }
    }
}
