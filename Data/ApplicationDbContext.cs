using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RaceDay.Models;

namespace RaceDay_Part2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RaceDay.Models.TblRoute> TblRoute { get; set; } = default!;
        public DbSet<RaceDay.Models.TblCategory> TblCategory { get; set; } = default!;
        public DbSet<RaceDay.Models.TblWeather> TblWeather { get; set; } = default!;
        public DbSet<RaceDay.Models.TblEvent> TblEvent { get; set; } = default!;
        public DbSet<RaceDay.Models.TblRegistration> TblRegistration { get; set; } = default!;
        public DbSet<RaceDay.Models.TblResult> TblResult { get; set; } = default!;
    }
}
