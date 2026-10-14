using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RaceDay_Part2.Data;
using System.ComponentModel.DataAnnotations;

[AllowAnonymous]
public class RegisterConfirmationModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _sender;

    public RegisterConfirmationModel(UserManager<ApplicationUser> userManager, IEmailSender sender)
    {
        _userManager = userManager;
        _sender = sender;
    }

    // ...rest unchanged (methods that use _userManager keep working)
}

[Authorize(Roles = "Admin")]
public class UserRolesController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserRolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
}


namespace RaceDay_Part2.Data
{
    public enum Gender
    {
        Male,
        Female,
        Other,
        PreferNotToSay
    }
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public Gender Gender { get; set; }

        [MaxLength(260)]
        public string ProfileImagePath { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";
    }
}



