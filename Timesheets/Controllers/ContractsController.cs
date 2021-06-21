using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Timesheets.Domain.Interfaces;
using Timesheets.Models.Dto;

namespace Timesheets.Controllers
{
    //[Authorize]
    public class ContractsController : TimesheetBaseController
    {
        private readonly IContractManager _contractManager;

        public ContractsController(IContractManager contractManager)
        {
            _contractManager = contractManager;
        }

        /// <summary> Возвращает информацию о контракте </summary>
        //[Authorize(Roles ="manager, user")]
        [HttpGet("ById")]
        public async Task<IActionResult> GetItem([FromQuery] Guid id)
        {
            var result = await _contractManager.GetItem(id);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        /// <summary> Возвращает информацию обо всех контрактах </summary>
        //[Authorize(Roles ="manager, user")]
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var result = await _contractManager.GetItems();
            return Ok(result);
        }

        /// <summary> Создает новый контракт</summary>
        //[Authorize(Roles = "manager")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContractCreateRequest contractRequest)
        {
            
            var id = await _contractManager.Create(contractRequest);
            return Ok(id);
        }

        /// <summary> Обновляет данные контракта </summary>
        //[Authorize(Roles = "manager")]
        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] Guid id, [FromBody] ContractCreateRequest contractRequest)
        {
            var succeed = await _contractManager.Update(id, contractRequest);
            if (!succeed)
            {
                return BadRequest($"Contract with Id \"{id}\" is not found.");
            }
            return Ok();
        }
    }
}