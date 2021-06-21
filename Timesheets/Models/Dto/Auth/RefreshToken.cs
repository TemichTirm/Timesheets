using System;

namespace Timesheets.Models
{
    /// <summary> Информация о пользователе системы </summary>
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }

    }
}