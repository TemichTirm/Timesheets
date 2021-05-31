using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.Data.Interfaces;
using Timesheets.Domain.Interfaces;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Domain.Implementation
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepo _employeeRepo;
        private readonly IUserRepo _userRepo;
        public EmployeeManager(IEmployeeRepo employeeRepo, IUserRepo userRepo)
        {
            _employeeRepo = employeeRepo;
            _userRepo = userRepo;
        }
        public async Task<Guid?> Create(EmployeeRequest employeeRequest)
        {
            var isUserExist = await _userRepo.CheckUserExist(null, employeeRequest.UserId);
            if (!isUserExist)
            {
                return null;
            }
            else 
            {
                var employees = await _employeeRepo.GetItems();
                foreach (var employee in employees)
                {
                    if (employee.UserId == employeeRequest.UserId)
                    {
                        return null;
                    }
                }
            } 
            var newEmployee = new Employee
            {
                Id = Guid.NewGuid(),
                Name = employeeRequest.Name,
                UserId = employeeRequest.UserId,
                IsDeleted = false
            };
            await _employeeRepo.Add(newEmployee);
            return newEmployee.Id;
        }

        public async Task<Employee> GetItem(Guid id)
        {
            return await _employeeRepo.GetItem(id);
        }

        public async Task<IEnumerable<Employee>> GetItems()
        {
            return await _employeeRepo.GetItems();
        }

        public async Task Update(Guid id, EmployeeRequest employeeRequest)
        {
            var employee = new Employee
            {
                Id = id,
                Name = employeeRequest.Name,
                UserId = employeeRequest.UserId,
                IsDeleted = employeeRequest.IsDeleted
            };
            await _employeeRepo.Update(employee);
        }
        public async Task<bool> CheckEmployeeExist(Guid id)
        {
            return await _employeeRepo.GetItem(id)!= null;
        }
    }
}
