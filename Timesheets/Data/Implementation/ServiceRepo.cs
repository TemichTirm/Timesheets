using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.Data.Interfaces;
using Timesheets.Models;

namespace Timesheets.Data.Implementation
{
    public class ServiceRepo : IServiceRepo
    {
        private readonly TimesheetDbContext _context;

        public ServiceRepo(TimesheetDbContext context)
        {
            _context = context;
        }
        public async Task Add(Service item)
        {
            await _context.Services.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Service> GetItem(Guid id)
        {
            var result = await _context.Services.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<Service>> GetItems()
        {
            var result = await _context.Services.ToListAsync();
            return result;
        }

        public async Task Update(Service item)
        {
            var result = await _context.Services.FindAsync(item.Id);
            result.Name = item.Name;
            _context.Services.Update(result);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> CheckServiceExist(string name, Guid? id = null)
        {
            Service result;
            if (id != null)
            {
                result = await _context.Services.FindAsync(id);
            }
            else
            {
                result = await _context.Services.AsQueryable().FirstOrDefaultAsync(s => s.Name == name);
            }
            return result != null;
        }
    }
}