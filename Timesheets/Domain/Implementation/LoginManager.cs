using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Timesheets.Data.Interfaces;
using Timesheets.Domain.Interfaces;
using Timesheets.Infrastructure.Extentions;
using Timesheets.Models;
using Timesheets.Models.Dto;
using Timesheets.Models.Dto.Auth;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Timesheets.Domain.Implementation
{
    public class LoginManager : ILoginManager

    {
        private readonly JwtAccessOptions _jwtAccessOptions;
        private readonly JwtRefreshOptions _jwtRefreshOptions;
        private readonly ITokenRepo _tokenRepo;
        private readonly IUserManager _userManager;


        public LoginManager(IOptions<JwtAccessOptions> jwtAccessOptions, IOptions<JwtRefreshOptions> jwtRefreshOptions, 
                            ITokenRepo tokenRepo, IUserManager userManager)
        {
            _jwtAccessOptions = jwtAccessOptions.Value;
            _jwtRefreshOptions = jwtRefreshOptions.Value;
            _tokenRepo = tokenRepo;
            _userManager = userManager;
        }
        
        /// <summary> Аутентификация пользователи и генерация токенов </summary>
        public async Task<LoginResponse> Authenticate(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };

            var accessTokenRaw = _jwtAccessOptions.GenerateToken(claims);
            var securityHandler = new JwtSecurityTokenHandler();
            var accessToken = securityHandler.WriteToken(accessTokenRaw);
            var refreshToken = await GenerateRefreshToken(claims);

            var loginResponse = new LoginResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = accessTokenRaw.ValidTo.ToEpochTime()
            };
            return loginResponse;
        }
        /// <summary> Создание refresh токена </summary>
        private async Task<string> GenerateRefreshToken(List<Claim> claims)
        {
            var refreshTokenRaw = _jwtRefreshOptions.GenerateToken(claims);
            var securityHandler = new JwtSecurityTokenHandler();
            var refreshToken = securityHandler.WriteToken(refreshTokenRaw);

            // Запись нового refresh токена в БД 
            var token = new RefreshToken { Id = Guid.NewGuid(), Token = refreshToken };
            await _tokenRepo.Add(token);
            return refreshToken;
        }

        /// <summary> Удаление refresh токена избазы данных </summary>
        public async Task DeleteRefreshToken(string token)
        {
            await _tokenRepo.Delete(token);
        }

        /// <summary> Получение пользователя по refresh токену </summary>
        public async Task<User> GetUser(string token)
        {
            _ = Guid.TryParse(DecodeToken(token).Claims.FirstOrDefault(c => c.Type == "sub").Value, out Guid userId);
            var user = await _userManager.GetItem(userId);
            return user;
        }

        /// <summary> Проверка валидности refresh токена </summary>
        public bool CheckRefreshTokenValidity(string token)
        {
            var now = DateTime.UtcNow;
            var expireInTS = TimeSpan.FromSeconds(long.Parse(DecodeToken(token).Claims.FirstOrDefault(c => c.Type == "exp").Value));
            var expireStart = new DateTime(1970, 1, 1);
            var expire = expireStart.Add(expireInTS);
            return now <= expire;
        }

        /// <summary> Декодирование токена </summary>
        private static JwtSecurityToken DecodeToken(string token)
        {
            var decodedToken = new JwtSecurityToken(token);
            return decodedToken;
        }

        /// <summary> Проверка наличия refresh токена в базе данных </summary>
        public async Task<bool> CheckRefreshToken(string token)
        {
            return await _tokenRepo.GetItem(token) != null;
        }
    }
}
