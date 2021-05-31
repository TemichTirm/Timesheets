using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Timesheets.Domain.Interfaces;

namespace Timesheets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary> Возвращает информацию о пользователе </summary>
        [HttpGet("ById")]
        public async Task<IActionResult> GetItem([FromQuery] Guid id)
        {
            var result = await _userManager.GetItem(id);

            return Ok(result);
        }

        /// <summary> Возвращает информацию обо всех пользователях </summary>
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var result = await _userManager.GetItems();
            return Ok(result);
        }

        /// <summary> Создает нового пользователя </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] string userName)
        {            
            var id = await _userManager.Create(userName);
            return Ok(id);
        }

        /// <summary> Обновляет имя пользователя </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] Guid id, [FromQuery] string userName)
        {
            var isUserNameExist = await _userManager.CheckUserExist(id);
            if (!isUserNameExist)
            {
                return BadRequest($"User with id {id} does not exists. It couldn't been updated.");
            }
            await _userManager.Update(id, userName);
            return Ok();
        }
    }
}