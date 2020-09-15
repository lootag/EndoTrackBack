using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Repositories.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Entities.Query;

namespace Persistence.Repositories.Implementations
{
    public class ProcessRepository : IProcessRepository
    {
        private readonly IContextFactory _contextFactory;

        public ProcessRepository(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public Entities.Process Get(long? processId)
        {
            if(processId == null) throw new ArgumentNullException(nameof(processId));
            using(var context = this._contextFactory.CreateContext())
            {
                if(processId == null) throw new ArgumentNullException(nameof(processId));
                var dto = context.Processes
                            .Where(p => p.ProcessId == processId)
                            .Include(p => p.Machine)
                                .ThenInclude(m => m.Customer)
                            .Single();
                var process = this.OrmEntityToBusinessEntity(dto);
                return process;    
            }
                    
        }   

        public IList<Entities.Process> GetAll()
        {
            using(var context = this._contextFactory.CreateContext())
            {
                var dtos = context.Processes
                            .Include(p => p.Machine)
                                .ThenInclude(m => m.Customer)
                            .ToList();
                var processes = OrmEntityListToBusinessEntityList(dtos);    

                return processes;
            }
            
        }

        public IList<Entities.Process> GetByQueryValues(ProcessQuery processQueryValues)
        {
            if(processQueryValues == null) throw new ArgumentNullException(nameof(processQueryValues));
            using(var context = this._contextFactory.CreateContext())
            {
                var processQuery = (from p in context.Processes select p);

                processQuery = processQuery
                                .Include(p => p.Machine)
                                    .ThenInclude(m => m.Customer);
                
                if(processQueryValues.MachineId != null)
                {
                    processQuery = processQuery
                                    .Where(p => p.MachineId == processQueryValues.MachineId);
                }

                if(processQueryValues.CustomerId != null)
                {
                    processQuery = processQuery
                                    .Where(p => p.Machine.CustomerId == processQueryValues.CustomerId);
                }

                if(processQueryValues.WaterTempMin != null && processQueryValues.WaterTempMax != null)
                {
                    processQuery = processQuery
                                    .Where(p => p.WaterTemp >= processQueryValues.WaterTempMin && p.WaterTemp <= processQueryValues.WaterTempMax);
                }

                if(processQueryValues.WaterLevelMlMin != null && processQueryValues.WaterLevelMlMax != null)
                {
                    processQuery = processQuery
                                    .Where(p => p.WaterLevelMl >= processQueryValues.WaterLevelMlMin && p.WaterLevelMl <= processQueryValues.WaterLevelMlMax);
                }

                if(processQueryValues.Pump10 != null)
                {
                    processQuery = processQuery
                                    .Where(p => p.Pump10 == processQueryValues.Pump10);
                }

                if(processQueryValues.Pump5 != null)
                {
                    processQuery = processQuery
                                    .Where(p => p.Pump5 == processQueryValues.Pump5);
                }

                if(processQueryValues.DrainSensor != null)
                {
                    processQuery = processQuery
                                    .Where(p => p.DrainSensor == processQueryValues.DrainSensor);
                }

                var dtos = processQuery
                                    .ToList();
                
                var processes = this.OrmEntityListToBusinessEntityList(dtos);
                return processes;
            }
        }

        private ORMEntities.Process BusinessEntityToOrmEntity(Entities.Process process)
        {
            return new ORMEntities.Process
            {
                ProcessId = process.Id,
                ProcessTimeStart = process.ProcessTimeStart,
                ProcessTimeEnd = process.ProcessTimeEnd,
                ProcessType = process.ProcessType,
                WaterTemp = process.WaterTemp,
                Pump10 = process.Pump10,
                Pump5 = process.Pump5,
                DrainSensor = process.DrainSensor,
                WaterLevelMl = process.WaterLevelMl,
                MachineId = process.MachineId
            };
        }

        

        private Entities.Process OrmEntityToBusinessEntity(ORMEntities.Process process)
        {
            var customer = new Entities.Customer(process.Machine.Customer.CustomerId, process.Machine.Customer.Name);
            var machine = new Entities.Machine(process.Machine.MachineId, 
                                               process.Machine.MachineNumber,
                                               process.Machine.OnlineFrom,
                                               process.Machine.MachineIdByCustomer,
                                               process.Machine.CustomerId,
                                               process.Machine.MachineType,
                                               process.Machine.SerialNumber,
                                               customer);
            return new Entities.Process(process.ProcessId,
                                        process.ProcessType,
                                        process.ProcessTimeStart,
                                        process.ProcessTimeEnd,
                                        process.MachineId,
                                        process.WaterTemp,
                                        process.Pump10,
                                        process.Pump5,
                                        process.DrainSensor,
                                        process.WaterLevelMl,
                                        machine);
        }

        private IList<Entities.Process> OrmEntityListToBusinessEntityList(List<ORMEntities.Process> dtos)
        {
            var processes = new List<Entities.Process>();
            foreach (var dto in dtos)
            {
                var process = this.OrmEntityToBusinessEntity(dto);
                processes.Add(process);
            }

            return processes;
        }


    }
}