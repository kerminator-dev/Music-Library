using Microsoft.EntityFrameworkCore;
using MusicDbApp.Models;

namespace MusicDbApp.DbContexts
{
    public class MusicDbContext : DbContext
    {
        /// <summary>
        /// Группы
        /// </summary>
        public DbSet<Group> Groups { get; set; }

        /// <summary>
        /// Альбомы
        /// </summary>
        public DbSet<Album> Albums { get; set; }

        /// <summary>
        /// Треки
        /// </summary>
        public DbSet<Song> Songs { get; set; }

        public MusicDbContext(DbContextOptions options) : base(options)
        {
            // Создание БД, если она не существует по указанному пути в options 
            base.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Albums)
                .WithOne();

            modelBuilder.Entity<Group>()
                .Navigation(g => g.Albums)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            modelBuilder.Entity<Album>()
                .HasMany(a => a.Songs)
                .WithOne();

            modelBuilder.Entity<Album>()
                .Navigation(a => a.Songs)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}
