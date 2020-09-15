using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Persistence.Repositories.Interfaces;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        

        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerRepository _customerRepository;
        private readonly IConfiguration _configuration;

        public CustomersController(ILogger<CustomersController> logger,
                              ICustomerRepository customerRepository,
                              IMachineRepository machineRepository,
                              IProcessRepository processRepository, 
                              IConfiguration configuration)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _configuration = configuration;
        }
        
        [HttpGet]
        [Route("get-all-customers")]
        public IActionResult GetAllCustomers()
        {
            var customers = this._customerRepository.GetAll();
            var dtos = this.BusinessEntityListToViewEntityList(customers);
            return Ok(dtos);
        }

        private ViewModels.Customer BusinessEntityToViewEntity(Entities.Customer customer)
        {
            return new ViewModels.Customer
            {
                Id = customer.Id,
                Name = customer.Name
            };
        }

        private IList<ViewModels.Customer> BusinessEntityListToViewEntityList(IList<Entities.Customer> customers)
        {
            var dtos = new List<ViewModels.Customer>();
            foreach(var customer in customers)
            {
                var dto = this.BusinessEntityToViewEntity(customer);
                dtos.Add(dto);
            }
            return dtos;
        }

    }
}
