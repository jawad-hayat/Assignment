using Business.Interfaces;
using Infrastructure.Models.Request;
using Infrastructure.Models.Response;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;
        public AuthService(UserManager<IdentityUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<AuthResponse> Login(LogInRequest request)
        {
            var userExist = await _userManager.FindByEmailAsync(request.Email);

            if (userExist == null)
            {
                return new AuthResponse()
                {
                    Errors = new List<string>() { "User not found" },
                    Success = false
                };
            }

            var isValid = await _userManager.CheckPasswordAsync(userExist, request.Password);

            if (!isValid)
            {
                return new AuthResponse()
                {
                    Errors = new List<string>() { "Invalid login request" },
                    Success = false
                };
            }
            

            var jwtToken = await _tokenService.GenerateJwtToken(userExist);
            return jwtToken;
        }
    }
}
