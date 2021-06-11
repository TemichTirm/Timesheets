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

        public Task<IEnumerable<Contract>> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task Add(Contract item)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Contract item)
        {
            _context.Contracts.Update(item);
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