using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Timesheets.Domain.Interfaces;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ContractController : ControllerBase
    {
        private readonly IContractManager _contractManager;

        public ContractController(IContractManager contractManager)
        {
            _contractManager = contractManager;
        }

        /// <summary> Возвращает информацию о контракте </summary>
        [Authorize(Roles ="manager, user")]
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
        [Authorize(Roles ="manager, user")]
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var result = await _contractManager.GetItems();
            return Ok(result);
        }

        /// <summary> Создает новый контракт</summary>
        [Authorize(Roles = "manager")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContractCreateRequest contractRequest)
        {
            
            var id = await _contractManager.Create(contractRequest);
            return Ok(id);
        }

        /// <summary> Обновляет данные контракта </summary>
        [Authorize(Roles = "manager")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Contract contract)
        {
            var isContractExist = await _contractManager.CheckContractExist(contract.Id);
            if (!isContractExist)
            {
                return NoContent();
            }
            await _contractManager.Update(contract);
            return Ok();
        }
    }
}