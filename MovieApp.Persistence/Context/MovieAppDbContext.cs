using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Persistence
{
    public class MovieAppDbContext : DbContext
    {
        public MovieAppDbContext(DbContextOptions<MovieAppDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<OfflineList> OfflineLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OfflineList>()
                .HasOne(ol => ol.Movie)
                .WithMany(m => m.OfflineLists)
                .HasForeignKey(ol => ol.MovieId);

            modelBuilder.Entity<Movie>()
            .HasMany(m => m.Genres)
            .WithMany()
            .UsingEntity(
                "MovieGenre",
                l => l.HasOne(typeof(Genre)).WithMany().HasForeignKey("GenreId"),
                r => r.HasOne(typeof(Movie)).WithMany().HasForeignKey("MovieId"),
                j => j.HasKey("MovieId", "GenreId"));
            
            base.OnModelCreating(modelBuilder);
        }
    }
}