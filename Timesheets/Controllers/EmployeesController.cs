using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Timesheets.Domain.Interfaces;
using Timesheets.Models.Dto;

namespace Timesheets.Controllers
{
    public class EmployeesController : TimesheetBaseController
    {
        private readonly IEmployeeManager _employeeManager;

        public EmployeesController(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        /// <summary> Возвращает информацию о сотруднике </summary>
        [HttpGet("ById")]
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
        public async Task<IActionResult> Create([FromBody] EmployeeCreateRequest employeeRequest)
        {
            var id = await _employeeManager.Create(employeeRequest);
            if (id != null)
            {
                return Ok(id);
            }
            else
            {
                return BadRequest($"UserId {employeeRequest.UserId} already connected with another Employee, or User with such Id doesn't exists");
            }
        }

        /// <summary> Обновляет данные сотрудника </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] Guid id, [FromBody] EmployeeCreateRequest employeeRequest)
        {

            var succeed = await _employeeManager.Update(id, employeeRequest);
            if (!succeed)
            {
                return BadRequest($"Employee with Id \"{id}\" is not found.");
            }
            return Ok();
        }

    }
}