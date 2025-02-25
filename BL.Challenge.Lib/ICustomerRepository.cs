using BL.Challenge.Lib.DTO;
using BL.Challenge.Lib.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Challenge.Lib
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync();
        Task<CustomerDTO> GetCustomerAsync(int id);
        Task<CustomerDTO> CreateCustomerAsync(Customer customer);
        Task<CustomerDTO> UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
    }
}
