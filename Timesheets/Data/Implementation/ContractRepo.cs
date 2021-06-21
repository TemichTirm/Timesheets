using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.Data.Interfaces;
using Timesheets.Models;

namespace Timesheets.Data.Implementation
{
    public class ContractRepo:IContractRepo
    {
        private readonly TimesheetDbContext _context;

        public ContractRepo(TimesheetDbContext context)
        {
            _context = context;
        }

        public async Task<Contract> GetItem(Guid id)
        {
            var result = await _context.Contracts.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<Contract>> GetItems()
        {
            var result = await _context.Contracts.ToListAsync();
            return result;
        }

        public async Task Add(Contract item)
        {
            await _context.Contracts.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Contract item)
        {
            var result = await _context.Contracts.FindAsync(item.Id);
            result.Title = item.Title;
            result.DateStart = item.DateStart;
            result.DateEnd = item.DateEnd;
            result.Description = item.Description;
            result.IsDeleted = item.IsDeleted;
            _context.Contracts.Update(result);
            await _context.SaveChangesAsync();
        }        

        public async Task<bool?> CheckContractIsActive(Guid id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            var now = DateTime.Now;
            var isActive = now <= contract?.DateEnd && now >= contract?.DateStart;

            return isActive;
        }
    }
}