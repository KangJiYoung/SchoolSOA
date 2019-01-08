using Microsoft.AspNetCore.Mvc;

namespace SchoolSOA.Services.Blog.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public string[] Index()
        {
            return new string[] { "Catcher Wong", "James Li" };
        }
    }
}