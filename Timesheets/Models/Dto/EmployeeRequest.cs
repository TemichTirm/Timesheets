using System;

namespace Timesheets.Models.Dto
{
    public class EmployeeRequest
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}