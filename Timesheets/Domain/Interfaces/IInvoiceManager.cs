using System;
using System.Threading.Tasks;
using Timesheets.Models.Dto;

namespace Timesheets.Domain.Interfaces
{
    public interface IInvoiceManager
    {
        Task<Guid?> Create(InvoiceCreateRequest invoiceRequest);
        Task<bool> Update(Guid id, InvoiceCreateRequest invoiceRequest);
    }
}
