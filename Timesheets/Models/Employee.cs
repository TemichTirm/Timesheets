using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timesheets.Models
{
    /// <summary> Информация о сотруднике </summary>
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }       
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
        
        public ICollection<Sheet> Sheets { get; set; }
        public User User { get; set; }
    }
}