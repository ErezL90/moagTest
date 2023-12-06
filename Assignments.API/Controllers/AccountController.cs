using Assignments.API.Account.BL;
using Assignments.API.Account.Models;
using Assignments.API.Account.Services;
using Assignments.API.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Assignments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterModel model)
        {
            var res = await _authService.RegisterUser(model);

            if (res.Succeeded)
            {
                return Ok(new { Response = $"{model.Email} {model.Username}"  });
            }
            else 
            {
                var errors = res.Errors.Select(x => x.Description);
                return BadRequest(new { Errors = errors });
            }

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Errors = "Is not valid" });
            }
            if (await _authService.LoginUser(model))
            {
                var token = _authService.GenerateTokenString(model);
                return Ok(new { token});
            }
            return BadRequest(new { Errors = "login error" });
        }

    }
}