using System;

namespace Timesheets.Models.Dto
{
    public class EmployeeCreateRequest
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}