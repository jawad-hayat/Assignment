using Business.Interfaces;
using Infrastructure.DataContext;
using Infrastructure.Models;
using Infrastructure.Models.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public TokenService(JwtConfig jwtConfig, UserManager<IdentityUser> userManager, TokenValidationParameters tokenValidationParameters, ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _jwtConfig = jwtConfig;
            _userManager = userManager;
            _tokenValidationParameters = tokenValidationParameters;
            _dbContext = dbContext;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<AuthResponse> GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var userRoles = await _userManager.GetRolesAsync(user);
            var role = await _roleManager.FindByNameAsync(userRoles[0]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, userRoles[0]),
                    new Claim("RoleId", role.Id)
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_jwtConfig.ExpirationTime)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);


            return new AuthResponse()
            {
                Token = jwtToken,
                Email = user.Email,
                UserId = user.Id,
                Name = user.UserName,
                Success = true,
            };
        }
    }
}
