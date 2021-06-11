using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.Data.Interfaces;
using Timesheets.Models;

namespace Timesheets.Data.Implementation
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly TimesheetDbContext _context;

        public EmployeeRepo(TimesheetDbContext context)
        {
            _context = context;
        }

        public async Task Add(Employee item)
        {
            await _context.Employees.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> GetItem(Guid id)
        {
            var result = await _context.Employees.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<Employee>> GetItems()
        {
            var result = await _context.Employees.ToListAsync();
            return result;
        }

        public async Task Update(Employee item)
        {            
            var result = await _context.Employees.FindAsync(item.Id);
            result.IsDeleted = item.IsDeleted;
            result.Name = item.Name;
            result.Sheets = item.Sheets;
            result.User = item.User;
            result.UserId = item.UserId;
            _context.Employees.Update(result);
            await _context.SaveChangesAsync();
        }
    }
}