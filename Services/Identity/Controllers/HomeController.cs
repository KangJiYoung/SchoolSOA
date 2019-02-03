using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SchoolSOA.Services.Identity.Entities;
using SchoolSOA.Services.Identity.ViewModels.Home;

namespace SchoolSOA.Services.Identity.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(
            [FromBody] LoginViewModel model,
            [FromServices] AuthDbContext dbContext)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var loginResult = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (!loginResult.Succeeded)
                return BadRequest(new { errors = "Invalid Username or Password" });

            var userId = dbContext.Users.First(it => it.UserName == model.UserName).Id;

            return Json(new UserViewModel { UserName = model.UserName, Token = GetToken(userId) });
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model, [FromServices] AuthDbContext dbContext)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var identityResult = await userManager.CreateAsync(new IdentityUser
            {
                UserName = model.UserName
            }, model.Password);

            if (!identityResult.Succeeded)
                return BadRequest(new { errors = string.Join(Environment.NewLine, identityResult.Errors.Select(it => it.Description)) });

            var userId = await dbContext.Users.Where(it => it.UserName == model.UserName).Select(it => it.Id).FirstAsync();

            return Json(new UserViewModel { UserName = model.UserName, Token = GetToken(userId) });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile([FromServices] AuthDbContext dbContext)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await dbContext.Users.FindAsync(userId);
            if (user == null)
                return Unauthorized();
            
            return Json(new UserViewModel { UserName = user.UserName, Token = GetToken(user.Id) });
        }
        
        private string GetToken(string userId)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("7ED0A2330F503C9887017387D1DBB52A9175DECEC88A8AB255E96E680A01C452"));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = "Issuer",
                ValidateAudience = true,
                ValidAudience = "Audience",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = false
            };

            var jwt = new JwtSecurityToken(
                issuer: "Issuer",
                audience: "Audience",
                claims: claims,
                notBefore: now,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}