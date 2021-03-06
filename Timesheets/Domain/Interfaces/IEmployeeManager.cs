using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Domain.Interfaces
{
    public interface IEmployeeManager
    {
        Task<Employee> GetItem(Guid id);
        Task<IEnumerable<Employee>> GetItems();
        Task<Guid?> Create(EmployeeCreateRequest employeeRequest);
        Task Update(Guid id, EmployeeCreateRequest employeeRequest);
        Task<bool> CheckEmployeeExist(Guid id);
    }
}