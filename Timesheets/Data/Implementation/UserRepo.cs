using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timesheets.Data.Interfaces;
using Timesheets.Models;

namespace Timesheets.Data.Implementation
{
    public class UserRepo: IUserRepo
    {
        private readonly TimesheetDbContext _context;

        public UserRepo(TimesheetDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetItem(Guid id)
        {
            var result = await _context.Users.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<User>> GetItems()
        {
            var result =  await _context.Users.ToListAsync();
            return result;
        }

        public async Task Add(User item)
        {
            await _context.Users.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User item)
        {
            var result = await _context.Users.FindAsync(item.Id);
            result.Username = item.Username;
            _context.Users.Update(result);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckUserExist(string userName, Guid? id = null)
        {
            User result;            
            if (id != null)
            {
                result = await _context.Users.FindAsync(id);
            }
            else
            {
                result = await _context.Users.AsQueryable().FirstOrDefaultAsync(u => u.Username == userName);
            }
            return result != null;
        }
    }
}