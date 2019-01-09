using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolSOA.Services.Blog.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public string[] Index()
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return new [] { email };
        }
    }
}