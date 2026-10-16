using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RaceDay_Part2.Models;

namespace RaceDay_Part2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }


        public DbSet<RaceDay_Part2.Models.TblRoute> TblRoute { get; set; } = default!;
        public DbSet<RaceDay_Part2.Models.TblCategory> TblCategory { get; set; } = default!;
        public DbSet<RaceDay_Part2.Models.TblWeather> TblWeather { get; set; } = default!;
        public DbSet<RaceDay_Part2.Models.TblEvent> TblEvent { get; set; } = default!;
        public DbSet<RaceDay_Part2.Models.TblRegistration> TblRegistration { get; set; } = default!;
        public DbSet<RaceDay_Part2.Models.TblResult> TblResult { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<ApplicationUser>()
                .Property(u => u.Gender)
                .HasConversion<string>()
                .HasMaxLength(20);
        }
    }
}
