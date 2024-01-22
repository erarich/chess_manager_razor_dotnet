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
        public DbSet<api_mvc.Models.TournamentViewModel> TournamentViewModel { get; set; } = default!;
        public DbSet<PlayerViewModel> PlayerViewModel { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da chave estrangeira
            modelBuilder.Entity<TournamentViewModel>()
                .HasOne(t => t.OwnerUser)
                .WithMany()
                .HasForeignKey(t => t.OwnerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlayerViewModel>()
                .HasOne(p => p.Tournament)
                .WithMany()
                .HasForeignKey(p => p.TournamentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
