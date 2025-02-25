using BL.Challenge.Lib;
using BL.Challenge.Lib.DTO;
using BL.Challenge.Lib.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace BL.Challenge.Rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _CustomerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _CustomerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            var customers = await _CustomerRepository.GetAllCustomersAsync();
            return customers == null ? NotFound() : Ok(customers);
        }

        // GET: api/customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(int id)
        {
            var customer = _CustomerRepository.GetCustomerAsync(id).Result;
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        // POST: api/customers
        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> PostCustomer(Customer customer)
        {
            var newCustomer = await _CustomerRepository.CreateCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = newCustomer.Id }, newCustomer);
        }

        // PUT: api/customers/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            var updatedCustomer = await _CustomerRepository.UpdateCustomerAsync(customer);
            if (updatedCustomer == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            await _CustomerRepository.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
