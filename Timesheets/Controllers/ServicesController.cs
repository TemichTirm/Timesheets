using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Timesheets.Domain.Interfaces;
using Timesheets.Models.Dto;

namespace Timesheets.Controllers
{
    public class ServicesController : TimesheetBaseController
    {
        private readonly IServiceManager _serviceManager;
        public ServicesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary> Создает новую услугу </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceCreateRequest serviceRequest)
        {
            var id = await _serviceManager.Create(serviceRequest);
            
            if (id == null)
            {
                return BadRequest($"Service with service Name \"{serviceRequest.Name}\" already exist!");
            }

            return Ok(id);
        }
    }
}
