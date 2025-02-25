using BL.Challenge.Lib.DTO;
using BL.Challenge.Lib.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Challenge.Lib
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(CustomerContext context, ILogger<CustomerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync()
        {
            return await _context.Customers
                .Select(c => new CustomerDTO
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    MiddleName = c.MiddleName,
                    LastName = c.LastName,
                    EmailAddress = c.EmailAddress,
                    Phone = c.Phone
                })
                .ToListAsync();
        }

        public async Task<CustomerDTO> GetCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return null;
            }

            return new CustomerDTO
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                MiddleName = customer.MiddleName,
                LastName = customer.LastName,
                EmailAddress = customer.EmailAddress,
                Phone = customer.Phone
            };
        }

        public async Task<CustomerDTO> CreateCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            _logger.Log(LogLevel.Information, "New customer created");
            return await GetCustomerAsync(customer.Id);
        }

        public async Task<CustomerDTO> UpdateCustomerAsync(Customer customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(customer.Id);
            if (existingCustomer == null)
            {
                return null;
            }

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.MiddleName = customer.MiddleName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.EmailAddress = customer.EmailAddress;
            existingCustomer.Phone = customer.Phone;

            await _context.SaveChangesAsync();
            return await GetCustomerAsync(customer.Id);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
