using Microsoft.EntityFrameworkCore;
using MoviesApp.Models;

namespace MoviesApp.Data
{
    public class MoviesContext : DbContext
    {
        public MoviesContext (DbContextOptions<MoviesContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actors> Actors { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost;Database=MoviesContext;Trusted_Connection=True;TrustServerCertificate=True");
            }
            base.OnConfiguring(optionsBuilder);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actors>(entity =>
            {
                entity.HasKey(pr => pr.ActorId).HasName("ActorId");
                entity.Property(ps => ps.FirstName).HasColumnName("FirstName")
                    .HasMaxLength(128)
                    .IsRequired();
                entity.Property(ps => ps.LastName).HasColumnName("LastName")
                    .HasMaxLength(128)
                    .IsRequired();
                entity.Property(e => e.BirthDate).HasColumnType("datetime2");
            });
        }
    }
}