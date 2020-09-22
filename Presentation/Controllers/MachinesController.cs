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
    [Route("api/machines")]
    public class MachinesController : ControllerBase
    {
        

        private readonly ILogger<MachinesController> _logger;
        private readonly IMachineRepository _machineRepository;
        private readonly IConfiguration _configuration;

        public MachinesController(ILogger<MachinesController> logger,
                              IMachineRepository machineRepository,
                              IConfiguration configuration)
        {
            _logger = logger;
            _machineRepository = machineRepository;
            _configuration = configuration;
        }
        
        [HttpGet]
        [Route("get-all-machines")]
        public IActionResult GetAllMachines()
        {
            try
            {
                var machines = this._machineRepository.GetAll();
                var dtos = this.BusinessEntityListToViewEntityList(machines);
                return Ok(dtos);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
            
        }

        private ViewModels.Machine BusinessEntityToViewEntity(Entities.Machine machine)
        {
            return new ViewModels.Machine
            {
                Id = machine.Id,
                SerialNumber = machine.SerialNumber
            };
        }

        private IList<ViewModels.Machine> BusinessEntityListToViewEntityList(IList<Entities.Machine> machines)
        {
            var dtos = new List<ViewModels.Machine>();
            foreach(var machine in machines)
            {
                var dto = this.BusinessEntityToViewEntity(machine);
                dtos.Add(dto);
            }
            return dtos;
        }

    }
}
