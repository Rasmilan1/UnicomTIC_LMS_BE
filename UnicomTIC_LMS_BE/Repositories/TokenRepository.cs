using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using UnicomTIC_LMS_BE.Entities;
using UnicomTIC_LMS_BE.DataBase;
using UnicomTIC_LMS_BE.IRepositories;

namespace UnicomTIC_LMS_BE.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly LMSDbContext _appDbContext;
        private readonly IConfiguration _configuration;
        public TokenRepository(LMSDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }
        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),  // Add user ID as a claim
                    new Claim(ClaimTypes.Role, user.UserRole?.Role?.RoleName ?? "Unknown"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken
            (
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials,
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
