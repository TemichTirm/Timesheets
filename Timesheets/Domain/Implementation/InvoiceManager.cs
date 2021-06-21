using System;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Data.Interfaces;
using Timesheets.Domain.Interfaces;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Domain.Implementation
{
    public class InvoiceManager : IInvoiceManager
    {
        private readonly ISheetRepo _sheetRepo;
        private readonly IInvoiceRepo _invoiceRepo;
        private const int Rate = 100;


        public InvoiceManager(ISheetRepo sheetRepo, IInvoiceRepo invoiceRepo)
        {
            _sheetRepo = sheetRepo;
            _invoiceRepo = invoiceRepo;
        }

        public async Task<Guid?> Create(InvoiceCreateRequest invoiceRequest)
        {
            var sheets = await _sheetRepo
                .GetItemsForInvoice(invoiceRequest.ContractId, invoiceRequest.DateStart, invoiceRequest.DateEnd);

            if (!sheets.Any())
            {
                return null;
            }

            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                ContractId = invoiceRequest.ContractId,
                DateStart = invoiceRequest.DateStart,
                DateEnd = invoiceRequest.DateEnd
            };
            invoice.Sheets.AddRange(sheets);
            invoice.Sum = invoice.Sheets.Sum(x => x.Amount * Rate);
            await _invoiceRepo.Add(invoice);

            return invoice.Id;
        }

        public async Task<bool> Update(Guid id, InvoiceCreateRequest invoiceRequest)
        {
            var invoice = await _invoiceRepo.GetItem(id);
            if (invoice == null)
            {
                return false;
            }
            foreach (var sheet in invoice.Sheets)
            {
                sheet.InvoiceId = null;
                await _sheetRepo.Update(sheet);
            }
            var newInvoice = new Invoice
            {
                Id = id,
                ContractId = invoiceRequest.ContractId,
                DateStart = invoiceRequest.DateStart,
                DateEnd = invoiceRequest.DateEnd
            };
            var sheets = await _sheetRepo
                .GetItemsForInvoice(invoiceRequest.ContractId, invoiceRequest.DateStart, invoiceRequest.DateEnd);
            newInvoice.Sheets.AddRange(sheets);
            newInvoice.Sum = newInvoice.Sheets.Sum(x => x.Amount * Rate);

            await _invoiceRepo.Update(newInvoice);
            return true;
        }
    }
}
