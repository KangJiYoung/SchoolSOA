using System.ComponentModel.DataAnnotations;

namespace SchoolSOA.Services.Identity.ViewModels.Home
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}