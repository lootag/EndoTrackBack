using Persistence;
using System.Collections.Generic;
using Entities;
using System.Threading.Tasks;

namespace Persistence.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        IList<Customer> GetAll();
    }
}