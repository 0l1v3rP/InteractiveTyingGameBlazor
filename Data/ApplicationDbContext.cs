using InteractiveTyingGameBlazor.DbModels;
using InteractiveTyingGameBlazor.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InteractiveTyingGameBlazor.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<TypingResult> TypingResults { get; set; }

        public DbSet<RegisteredVideo> RegisteredVideos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RegisteredVideo>()
                .Property(e => e.Category)
                .HasConversion<string>();
        }
    }
}   
