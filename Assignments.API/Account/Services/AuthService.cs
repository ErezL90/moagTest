using Assignments.API.Account.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assignments.API.Account.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<IdentityResult> RegisterUser(RegisterModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.Username,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }

        public async Task<bool> LoginUser(LoginModel model)
        {
            var identityUser = await _userManager.FindByEmailAsync(model.Email);
            
            if(identityUser == null)
            {
                return false;
            }
            return await _userManager.CheckPasswordAsync(identityUser, model.Password);

        }

        public string GenerateTokenString(LoginModel model)
        {
            var claims = new List<Claim> 
            { 
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Role, "Admin"),
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value!));
            SigningCredentials signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(
                claims:claims,
                expires: DateTime.Now.AddMinutes(120),
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCred
                );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }
    }
}