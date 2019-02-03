using System.ComponentModel.DataAnnotations;

namespace SchoolSOA.Services.Identity.ViewModels.Home
{
    public class UpdateFullNameViewModel
    {
        [Required]
        public string FullName { get; set; }
    }
}