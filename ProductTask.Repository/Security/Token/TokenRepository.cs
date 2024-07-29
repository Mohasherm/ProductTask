using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductTask.Repository.Security.Token.Dto;
using ProductTask.SqlServer.Data;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProductTask.Repository.Security.Token
{


    public class TokenRepository : ITokenRepository
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;
        public async Task<TokenDto> GenerateJwtToken(Guid userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(s => s.IsValid && s.Id == userId);
            var role = await context.Roles.FirstOrDefaultAsync(s => s.IsValid && s.Id == user.RoleId);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Key").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, role.Name.ToString()),
        };

            var token = new JwtSecurityToken(

                issuer: "https://localhost:5160/",
                audience: "https://localhost:7164/",
                claims,
                expires: DateTime.UtcNow.AddYears(1),
                signingCredentials: credentials);

            var tokeninfo = new TokenDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = GenerateRefreshToken(),
                UserId = user.Id,

            };

            return tokeninfo;
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}