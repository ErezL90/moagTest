using Assignments.API.Account.Models;
using Microsoft.AspNetCore.Identity;

namespace Assignments.API.Account.Services
{
    public interface IAuthService
    {
        string GenerateTokenString(LoginModel model);
        Task<bool> LoginUser(LoginModel model);
        Task<IdentityResult> RegisterUser(RegisterModel model);
    }
}