using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Timesheets.Data.Interfaces;
using Timesheets.Models;

namespace Timesheets.Data.Implementation
{
    public class TokenRepo : ITokenRepo
    {
        private readonly TimesheetDbContext _context;
        public TokenRepo(TimesheetDbContext context)
        {
            _context = context;
        }

        /// <summary> Запись токена в базу данных </summary>
        public async Task Add(RefreshToken item)
        {
            await _context.Tokens.AddAsync(item);
            await _context.SaveChangesAsync();
        }


        /// <summary> Удаление токена из базы данных </summary>
        public async Task Delete(string item)
        {
            var token = await _context.Tokens.FirstOrDefaultAsync(t => t.Token == item);
            _context.Tokens.Remove(token);
            await _context.SaveChangesAsync();

        }


        /// <summary> Получение сущности токена из базы данных </summary>
        public async Task<RefreshToken> GetItem(string item)
        {
            var result = await _context.Tokens.AsQueryable().FirstOrDefaultAsync(u => u.Token == item);
            return result;
        }
    }
}
