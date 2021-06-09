using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Models;

namespace Timesheets.Data.Interfaces
{
    public interface ITokenRepo 
    {
        Task<RefreshToken> GetItem(string item);
        Task Add(RefreshToken item);
        Task Delete(string item);
    }
}
