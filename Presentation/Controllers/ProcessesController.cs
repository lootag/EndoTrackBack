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
    [Route("api/processes")]
    public class ProcessesController : ControllerBase
    {
        

        private readonly ILogger<ProcessesController> _logger;
        private readonly IProcessRepository _processRepository;
        private readonly IConfiguration _configuration;

        public ProcessesController(ILogger<ProcessesController> logger,
                              IProcessRepository processRepository,
                              IConfiguration configuration)
        {
            _logger = logger;
            _processRepository = processRepository;
            _configuration = configuration;
        }
        
        [HttpGet]
        [Route("get-all-processes")]
        public IActionResult GetAllProcesses()
        {
            try
            {
                var processes = this._processRepository.GetAll();
                var dtos = this.BusinessEntityListToViewInfoEntityList(processes);
                return Ok(dtos);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
            
        }

        [HttpGet]
        [Route("get-process/{id}")]
        public IActionResult GetProcess(long? id)
        {
            if(id == null) return BadRequest("The client passed a null process Id");
            try
            {
                var process = this._processRepository.Get(id);
                var dto = this.BusinessEntityToViewEntity(process);
                return Ok(dto);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("get-processes")]
        public IActionResult GetProcesses(ViewModels.ProcessQuery dtoIn)
        {
            if(dtoIn == null) return BadRequest("The client passed a null process request");
            try
            {
                var processQuery = this.ViewQueryEntityToBusinessQueryEntity(dtoIn);
                var processes = this._processRepository.GetByQueryValues(processQuery);
                var dtoOut = this.BusinessEntityListToViewInfoEntityList(processes);
                return Ok(dtoOut);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        private ViewModels.Process BusinessEntityToViewEntity(Entities.Process process)
        {
            var customer = new ViewModels.Customer
            {
                Id = process.Machine.Customer.Id,
                Name = process.Machine.Customer.Name
            };
            var machine = new ViewModels.Machine
            {
                Id = process.Machine.Id,
                CustomerId = process.Machine.CustomerId,
                Customer = customer,
                MachineIdByCustomer = process.Machine.MachineIdByCustomer,
                OnlineFrom = process.Machine.OnlineFrom,
                MachineNumber = process.Machine.MachineNumber,
                MachineType = (int)process.Machine.MachineType,
                SerialNumber = process.Machine.SerialNumber
            };

            return new ViewModels.Process
            {
                Id = process.Id,
                ProcessType = (int)process.ProcessType,
                ProcessTimeStart = process.ProcessTimeStart,
                ProcessTimeEnd = process.ProcessTimeEnd,
                WaterTemp = process.WaterTemp,
                WaterLevelMl = process.WaterLevelMl,
                Pump10 = process.Pump10,
                Pump5 = process.Pump5,
                DrainSensor = process.DrainSensor,
                MachineId = process.MachineId,
                Machine = machine
            };
        }

        private IList<ViewModels.Process> BusinessEntityListToViewEntityList(IList<Entities.Process> processes)
        {
            var dtos = new List<ViewModels.Process>();
            foreach(var process in processes)
            {
                var dto = this.BusinessEntityToViewEntity(process);
                dtos.Add(dto);
            }
            return dtos;
        }

        private ViewModels.Process BusinessEntityToViewInfoEntity(Entities.Process process)
        {
            return new ViewModels.Process
            {
                Id = process.Id,
                ProcessTimeStart = process.ProcessTimeStart,
                ProcessTimeEnd = process.ProcessTimeEnd
            };
        }

        private IList<ViewModels.Process> BusinessEntityListToViewInfoEntityList(IList<Entities.Process> processes)
        {
            var dtos = new List<ViewModels.Process>();
            foreach(var process in processes)
            {
                var dto = this.BusinessEntityToViewInfoEntity(process);
                dtos.Add(dto);
            }
            return dtos;
        }

        private Entities.Query.ProcessQuery ViewQueryEntityToBusinessQueryEntity(ViewModels.ProcessQuery processQuery)
        {
            return new Entities.Query.ProcessQuery(processQuery.WaterTempMin,
                                                   processQuery.WaterTempMax,
                                                   processQuery.Pump10,
                                                   processQuery.Pump5,
                                                   processQuery.DrainSensor,
                                                   processQuery.WaterLevelMlMin,
                                                   processQuery.WaterLevelMlMax,
                                                   processQuery.MachineId,
                                                   processQuery.CustomerId);
        }

        

    }
}
