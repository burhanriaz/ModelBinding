using Microsoft.EntityFrameworkCore;
using ModelBinding.Models;

namespace ModelBinding.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<StudentInfo> StudentInfo { get; set; }
    }
}
