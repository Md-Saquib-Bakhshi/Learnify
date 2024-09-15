using LMSApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LMSApi.Data
{
    public class LMSDbContext : DbContext
    {
        public LMSDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<MeetingRequest> MeetingRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Playlist>()
                .HasMany(p => p.Courses)
                .WithOne(c => c.Playlist)
                .HasForeignKey(c => c.PlaylistId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
