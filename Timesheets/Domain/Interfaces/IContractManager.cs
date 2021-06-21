using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Domain.Interfaces
{
    public interface IContractManager
    {
        Task<bool?> CheckContractIsActive(Guid id);
        Task<Contract> GetItem(Guid id);
        Task<IEnumerable<Contract>> GetItems();
        Task<Guid> Create(ContractCreateRequest contractRequest);
        Task<bool> Update(Guid id, ContractCreateRequest contractRequest);
    }
}