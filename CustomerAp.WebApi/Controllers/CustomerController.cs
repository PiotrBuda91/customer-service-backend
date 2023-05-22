using CustomerApi.Domain.DTOs;
using CustomerApi.Domain.Interfaces;
using CustomerApi.Infrastructure.Data;
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
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            return _customerRepository.GetCustomers().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(string id)
        {
            var customer = _customerRepository.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            _customerRepository.AddCustomer(customer);

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(string id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _customerRepository.UpdateCustomer(customer);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(string id)
        {
            var customer = _customerRepository.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }

            _customerRepository.DeleteCustomer(customer);

            return NoContent();
        }
    }
}