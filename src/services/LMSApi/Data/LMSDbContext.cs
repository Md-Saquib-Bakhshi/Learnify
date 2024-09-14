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
    }
}
