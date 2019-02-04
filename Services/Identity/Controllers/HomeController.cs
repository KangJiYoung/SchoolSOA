using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Common.Events;
using MassTransit;
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
        private readonly UserManager<AuthUser> userManager;
        private readonly SignInManager<AuthUser> signInManager;
        private readonly AuthDbContext dbContext;
        private readonly IBus bus;

        public HomeController(
            UserManager<AuthUser> userManager, 
            SignInManager<AuthUser> signInManager,
            AuthDbContext dbContext,
            IBus bus)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.dbContext = dbContext;
            this.bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Login(
            [FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var loginResult = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (!loginResult.Succeeded)
                return BadRequest(new {errors = "Invalid Username or Password"});

            var user = await dbContext.Users.FirstAsync(it => it.UserName == model.UserName);

            return Json(new UserViewModel {Username = model.UserName, Token = GetToken(user.Id, user.FullName)});
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var identityResult = await userManager.CreateAsync(new AuthUser
            {
                UserName = model.UserName,
                FullName = model.UserName
            }, model.Password);

            if (!identityResult.Succeeded)
                return BadRequest(new
                    {errors = string.Join(Environment.NewLine, identityResult.Errors.Select(it => it.Description))});

            var userId = await dbContext
                .Users
                .Where(it => it.UserName == model.UserName)
                .Select(it => it.Id)
                .FirstAsync();

            return Json(new UserViewModel
                {Username = model.UserName, Fullname = model.UserName, Token = GetToken(userId, model.UserName)});
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await dbContext.Users.FindAsync(userId);
            if (user == null)
                return Unauthorized();

            return Json(new UserViewModel
                {Username = user.UserName, Fullname = user.FullName, Token = GetToken(user.Id, user.FullName)});
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateFullName([FromBody] UpdateFullNameViewModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await dbContext.Users.FindAsync(userId);
            if (user == null)
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            user.FullName = model.FullName;
            dbContext.Update(user);
            await dbContext.SaveChangesAsync();

            await bus.Publish(new UserChangedFullNameEvent(new Guid(user.Id), user.FullName));

            return Json(new UserViewModel
                {Username = user.UserName, Fullname = user.FullName, Token = GetToken(user.Id, user.FullName)});
        }

        private static string GetToken(string userId, string fullName)
        {
            var now = DateTime.UtcNow;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, fullName),
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var signingKey =
                new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes("7ED0A2330F503C9887017387D1DBB52A9175DECEC88A8AB255E96E680A01C452"));


            var jwt = new JwtSecurityToken("Issuer", "Audience", claims, now,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}