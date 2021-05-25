using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Timesheets.Domain.Interfaces;
using Timesheets.Models.Dto;

namespace Timesheets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeManager _employeeManager;

        public EmployeesController(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        /// <summary> Возвращает информацию о сотруднике </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem([FromQuery] Guid id)
        {
            var result = await _employeeManager.GetItem(id);
            return Ok(result);
        }

        /// <summary> Возвращает информацию обо всех сотрудниках </summary>
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var result = await _employeeManager.GetItems();
            return Ok(result);
        }

        /// <summary> Создает нового сотрудника </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeRequest employeeRequest)
        {           
            var id = await _employeeManager.Create(employeeRequest);
            return Ok(id);
        }

        /// <summary> Обновляет данные сотрудника </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] EmployeeRequest employeeRequest)
        {
            var isEmployeeExist = await _employeeManager.CheckEmployeeExist(id);
            if (!isEmployeeExist)
            {
                return BadRequest($"Employee with id {id} does not exists. It couldn't been updated.");
            }
            await _employeeManager.Update(id, employeeRequest);
            return Ok();
        }

    }
}