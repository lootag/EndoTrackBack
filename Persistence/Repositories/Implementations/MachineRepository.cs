using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Repositories.Interfaces;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Implementations
{
    public class MachineRepository : IMachineRepository
    {
        private readonly IContextFactory _contextFactory;

        public MachineRepository(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        
        public IList<Entities.Machine> GetAll()
        {
            using(var context = this._contextFactory.CreateContext())
            {
                var dtos = context.Machines
                            .Include(m => m.Customer)
                            .ToList();
                var machines = new List<Entities.Machine>();
                foreach(var dto in dtos)
                {
                    var machine = this.OrmEntityToBusinessEntity(dto);
                    machines.Add(machine);
                }

                return machines;
            }
            
        }


        private Entities.Machine OrmEntityToBusinessEntity(ORMEntities.Machine machine)
        {
            var customer = new Entities.Customer(machine.Customer.CustomerId, machine.Customer.Name);
            return new Entities.Machine(machine.MachineId, 
                                        machine.MachineNumber,
                                        machine.OnlineFrom,
                                        machine.MachineIdByCustomer,
                                        machine.CustomerId,
                                        machine.MachineType,
                                        machine.SerialNumber,
                                        customer);
                                        
        }
    }
}