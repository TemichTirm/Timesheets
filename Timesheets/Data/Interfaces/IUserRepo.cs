using System;
using System.Threading.Tasks;
using Timesheets.Models;

namespace Timesheets.Data.Interfaces
{
    public interface IUserRepo: IRepoBase<User>
    {
        public Task<bool> CheckUserExist(string userName, Guid? id = null);
    }
}