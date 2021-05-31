using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Data.Interfaces;
using Timesheets.Domain.Interfaces;
using Timesheets.Models;

namespace Timesheets.Domain.Implementation
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepo _userRepo;
        public UserManager(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<Guid> Create(string userName)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = userName
            };
            await _userRepo.Add(user);
            return user.Id;
        }

        public async Task<User> GetItem(Guid id)
        {
            return await _userRepo.GetItem(id);
        }

        public async Task<IEnumerable<User>> GetItems()
        {
            return await _userRepo.GetItems();
        }

        public async Task Update(Guid id, string userName)
        {
            var user = new User
            {
                Id = id,
                Username = userName
            };
            await _userRepo.Update(user);
        }
        public async Task<bool> CheckUserExist(Guid id)
        {
            return await _userRepo.GetItem(id) != null;
        }
    }
}
