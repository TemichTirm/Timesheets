using System;
using System.Threading.Tasks;
using Timesheets.Models;

namespace Timesheets.Data.Interfaces
{
    public interface IServiceRepo: IRepoBase<Service>
    {
        public Task<bool> CheckServiceExist(string name, Guid? id = null);

    }
}