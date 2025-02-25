using BL.Challenge.Lib;
using BL.Challenge.Lib.DTO;
using BL.Challenge.Lib.Schema;
using Microsoft.Testing.Platform.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BL.Challenge.Test
{
    [TestClass]
    public class UnitTests
    {
        private CustomerRepository _customerRepository;

        [TestInitialize]
        public void Initialize()
        {
            var context = new CustomerContext();
            var logger = new PipelineLogger<CustomerRepository>();
            _customerRepository = new CustomerRepository(context, logger);
        }

        [TestMethod]
        public void InsertCustomer()
        {
            var newCustomer = new Customer()
            {
                EmailAddress = "testUser@emailAddress.com",
                FirstName = "First-Name",
                LastName = "Last-Name",
                MiddleName = "",
            };

            var customer = _customerRepository.CreateCustomerAsync(newCustomer).Result;
            Assert.IsTrue(customer.Id >= 1);
        }

        [TestMethod]
        public void UpdateCustomer()
        {   
            var newCustomer = new Customer()
            {
                EmailAddress = "testUser@emailAddress.com",
                FirstName = "First-Name",
                LastName = "Last-Name",
                MiddleName = "",
            };

            var customer = _customerRepository.CreateCustomerAsync(newCustomer).Result;

            newCustomer.FirstName = "Second-Name";
            var update = _customerRepository.UpdateCustomerAsync(newCustomer).Result;

            Assert.AreNotEqual(customer.FirstName, update.FirstName);           
        }
    }
}
