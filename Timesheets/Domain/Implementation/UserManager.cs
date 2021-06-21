using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Timesheets.Data.Interfaces;
using Timesheets.Domain.Interfaces;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Domain.Implementation
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepo _userRepo;
        public UserManager(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<Guid?> Create(UserCreateRequest request)
        {
            // Проверка, используется ли уже имя пользователя.
            if (await _userRepo.CheckUserExist(request.Username))
            {
                return null;
            }

            // Если имя пользователя уникальное - регистрация нового пользователя.
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                PasswordHash = GetPasswordHash(request.Password),
                Role = request.Role
            };
            await _userRepo.Add(newUser);
            return newUser.Id;
        }

        public async Task<User> GetItem(Guid id)
        {
            return await _userRepo.GetItem(id);
        }

        public async Task<IEnumerable<User>> GetItems()
        {
            return await _userRepo.GetItems();
        }

        public async Task Update(User user)
        {
            await _userRepo.Update(user);
        }
        public async Task<bool> CheckUserExist(Guid id)
        {
            return await _userRepo.GetItem(id) != null;
        }
        public async Task<bool> CheckUserExist(string userName)
        {
            return await _userRepo.CheckUserExist(userName);
        }

        public async Task<User> GetUser(LoginRequest request)
        {
            var passwordHash = GetPasswordHash(request.Password);
            var user = await _userRepo.GetByLoginAndPasswordHash(request.Login, passwordHash);
            return user;
        }
        private static byte[] GetPasswordHash(string password)
        {
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                return sha1.ComputeHash(Encoding.Unicode.GetBytes(password));
            }
        }
    }
}
