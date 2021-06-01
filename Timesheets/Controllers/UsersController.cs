using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Timesheets.Domain.Interfaces;
using Timesheets.Models;
using Timesheets.Models.Dto;

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
            if (result == null)
            {
                return NoContent();
            }
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
        public async Task<IActionResult> Create([FromBody] UserCreateRequest user)
        {
            
            var id = await _userManager.Create(user);
            // Проверка, используется ли уже имя пользователя
            if (id == null) 
            {
                return BadRequest($"User with Username {user.Username} already registered. Please use another Username");
            }
            return Ok(id);
        }

        /// <summary> Обновляет данные пользователя </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            var isUserExist = await _userManager.CheckUserExist(user.Id);
            if (!isUserExist)
            {
                return NoContent();
            }
            await _userManager.Update(user);
            return Ok();
        }
    }
}