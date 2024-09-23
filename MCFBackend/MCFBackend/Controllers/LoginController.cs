using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using MCFBackend.Context;
using MCFBackend.Services.Dto;
using MCFBackend.Services.Interfaces;
using MCFBackend.Services.Helper;

namespace MCFBackend.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogin iLoginRepo;
        public LoginController(ILogin loginRepo) => this.iLoginRepo = loginRepo;
        [HttpPost("api/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await iLoginRepo.GetUserByCredentialsAsync(request.user_name, request.password);

            if (user == null || !user.is_active)
            {
                return Unauthorized();
            }
            var claims = new List<Claim>{
                new 
                Claim(ClaimTypes.Name, user.user_name)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync("Cookies", claimsPrincipal);
            return Ok();
        }
    }
}
