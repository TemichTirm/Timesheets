using System;

namespace Timesheets.Models.Dto
{
    public class UserCreateRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}