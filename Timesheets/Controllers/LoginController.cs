using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Timesheets.Domain.Interfaces;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ILoginManager _loginManager;

        public LoginController(IUserManager userManager, ILoginManager loginManager)
        {
            _userManager = userManager;
            _loginManager = loginManager;
        }
        /// <summary> Первичная аутентификация пользователя с помощью логина и пароля </summary>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.GetUser(request);
            if (user == null)
            {
                return Unauthorized();
            }
            var loginResponse = await _loginManager.Authenticate(user);
            return Ok(loginResponse);
        }
        /// <summary> Повторная аутентификация при помощи refresh-token </summary>
        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromQuery] string token)
        {
            // Проверка наличия refresh токена в базе данных
            var isRefreshTokenExist = await _loginManager.CheckRefreshToken(token);
            if (!isRefreshTokenExist)
            {
                return BadRequest();
            }

            // Проверка валидности токена
            var isRefreshTokenValid = _loginManager.CheckRefreshTokenValidity(token);
            var user = await _loginManager.GetUser(token);
            await _loginManager.DeleteRefreshToken(token);
            if (!isRefreshTokenValid || user == null)
            {
                return BadRequest();
            }

            // Выпуск новых access и refresh токенов
            var loginResponse = await _loginManager.Authenticate(user);
            return Ok(loginResponse);
        }
    }
}
