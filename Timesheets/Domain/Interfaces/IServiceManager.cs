using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Domain.Interfaces
{
    public interface IServiceManager
    {
        Task<Service> GetItem(Guid id);
        Task<IEnumerable<Service>> GetItems();
        Task<Guid?> Create(ServiceCreateRequest serviceRequest);
        Task<bool> Update(Guid id, ServiceCreateRequest serviceRequest);
    }
}