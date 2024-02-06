using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using api_mvc.Models;

namespace api_mvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<api_mvc.Models.Tournament> TournamentViewModel { get; set; } = default!;
        public DbSet<Player> PlayerViewModel { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da chave estrangeira
            modelBuilder.Entity<Tournament>()
                .HasOne(t => t.OwnerUser)
                .WithMany()
                .HasForeignKey(t => t.OwnerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Player>()
                .HasOne(p => p.Tournament)
                .WithMany()
                .HasForeignKey(p => p.TournamentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
