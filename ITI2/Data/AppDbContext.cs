using ITI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ITI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Course> Courses { set; get; }
        public DbSet<CourseResult> CourseResults { set; get; }
        public DbSet<Department> Departments { set; get; }
        public DbSet<Instructor> Instructors { set; get; }
        public DbSet<Trainee> Trainees { set; get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var constr = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(constr);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instructor>().HasIndex(i => i.Name);
            modelBuilder.Entity<Department>().HasIndex(i => i.Name);
            modelBuilder.Entity<Course>().HasIndex(i => i.Name);
            modelBuilder.Entity<Trainee>().HasIndex(i => i.Name);
        }
    }
}
