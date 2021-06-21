using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.Data.Interfaces;
using Timesheets.Models;

namespace Timesheets.Data.Implementation
{
    public class InvoiceRepo : IInvoiceRepo
    {
        private readonly TimesheetDbContext _context;
        public InvoiceRepo(TimesheetDbContext context)
        {
            _context = context;
        }
        public async Task Add(Invoice item)
        {
            await _context.Invoices.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Invoice> GetItem(Guid id)
        {
            var result = await _context.Invoices.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<Invoice>> GetItems()
        {
            var result = await _context.Invoices.ToListAsync();
            return result;
        }

        public async Task Update(Invoice item)
        {
            var result = await _context.Invoices.FindAsync(item.Id);
            result.ContractId = item.ContractId;
            result.DateStart = item.DateStart;
            result.DateEnd = item.DateEnd;
            result.Sum = item.Sum;
            await _context.SaveChangesAsync();
        }
    }
}
