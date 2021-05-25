using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.Models;

namespace Timesheets.Domain.Interfaces
{
    public interface IUserManager
    {
        Task<User> GetItem(Guid id);
        Task<IEnumerable<User>> GetItems();
        Task<Guid> Create(string userName);
        Task Update(Guid id, string userName);
        Task<bool> CheckUserExist(string userName, Guid? id = null);
    }
}