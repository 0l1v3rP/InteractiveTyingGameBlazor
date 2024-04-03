using InteractiveTyingGameBlazor.DbModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InteractiveTyingGameBlazor.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<TypingResult> TypingResults { get; set; }
        public DbSet<RegisteredVideo> RegisteredVideos { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RegisteredVideo>()
                .Property(e => e.Category)
                .HasConversion<string>();

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Recipent)
                .WithMany()
                .HasForeignKey(m => m.RecipentId)
                .OnDelete(DeleteBehavior.ClientSetNull); 
        }
    }
}   
