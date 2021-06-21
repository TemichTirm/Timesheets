using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Timesheets.Models.Dto.Auth
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SigningKey { get; set; } 
        public int LifeTime { get; set; }
        public TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidIssuer = Issuer,
                ValidateAudience = false,
                ValidAudience = Audience,
                ValidateLifetime = true,
                IssuerSigningKey = GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
                RoleClaimType = ClaimsIdentity.DefaultRoleClaimType
            };
        }

        private SecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SigningKey));
        }
        public JwtSecurityToken GenerateToken(IEnumerable<Claim> claimes)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken
            (
                issuer: Issuer,
                audience: Audience,
                claims: claimes,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(LifeTime)),
                signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );
            return jwt;
        }
    }
}
