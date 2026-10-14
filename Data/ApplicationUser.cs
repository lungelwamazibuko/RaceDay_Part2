using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;    
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
