using eSchool.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace eSchool.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<Grade>? Grades { get; set; }
        public DbSet<Subject>? Subjects { get; set; }
    }
}
