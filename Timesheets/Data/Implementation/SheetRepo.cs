using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timesheets.Data.Interfaces;
using Timesheets.Models;

namespace Timesheets.Data.Implementation
{
    public class SheetRepo: ISheetRepo
    {
        private readonly TimesheetDbContext _context;

        public SheetRepo(TimesheetDbContext context)
        {
            _context = context;
        }

        public async Task<Sheet> GetItem(Guid id)
        {
            var result = await _context.Sheets.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<Sheet>> GetItems()
        {
            var result =  await _context.Sheets.ToListAsync();
            return result;
        }

        public async Task Add(Sheet item)
        {
            await _context.Sheets.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Sheet item)
        {
            
            var result = await _context.Sheets.FindAsync(item.Id);
            result.Amount = item.Amount;
            result.Contract = item.Contract;
            result.ContractId = item.ContractId;
            result.Date = item.Date;
            result.Employee = item.Employee;
            result.EmployeeId = item.EmployeeId;
            result.Invoice = item.Invoice;
            result.InvoiceId = item.InvoiceId;
            result.Service = item.Service;
            result.ServiceId = item.ServiceId;
            _context.Sheets.Update(result);
            await _context.SaveChangesAsync();
        }
    }
}