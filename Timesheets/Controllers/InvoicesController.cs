using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Timesheets.Domain.Interfaces;
using Timesheets.Models.Dto;

namespace Timesheets.Controllers
{
    public class InvoicesController : TimesheetBaseController
    {
        private readonly IContractManager _contractManager;
        private readonly IInvoiceManager _invoiceManager;
        public InvoicesController(IContractManager contractManager, IInvoiceManager invoiceManager)
        {
            _contractManager = contractManager;
            _invoiceManager = invoiceManager;
        }

        /// <summary> Создает новый клиентский счет </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvoiceCreateRequest invoiceRequest)
        {
            var isAllowedToCreate = await _contractManager.CheckContractIsActive(invoiceRequest.ContractId);

            if (isAllowedToCreate != null && !(bool)isAllowedToCreate)
            {
                return BadRequest($"Contract {invoiceRequest.ContractId} is not active or not found.");
            }

            var id = await _invoiceManager.Create(invoiceRequest);
            
            if (id == null)
            {
                return BadRequest($"There is no any sheets for this time period or all of them already have been used for another invoices.");
            }

            return Ok(id);
        }
    }
}
