using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Repositories.Interfaces;
using System.Linq;
using System;

namespace Persistence.Repositories.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IContextFactory _contextFactory;

        public CustomerRepository(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public IList<Entities.Customer> GetAll()
        {
            using(var context = this._contextFactory.CreateContext())
            {
                var dtos = context.Customers
                        .ToList();
                var customers = new List<Entities.Customer>();
                foreach(var dto in dtos)
                {
                    var customer = this.OrmEntityToBusinessEntity(dto);
                    customers.Add(customer);
                }
                return customers;
            }
            
        }

        private Entities.Customer OrmEntityToBusinessEntity(ORMEntities.Customer customer)
        {
            return new Entities.Customer(customer.CustomerId, customer.Name);
        }

    }
}