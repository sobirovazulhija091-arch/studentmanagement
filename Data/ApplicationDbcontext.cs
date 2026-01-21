using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Npgsql;
public class ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options):DbContext(options)
{
     public DbSet<Enrollment> Enrollments{get;set;}
     public DbSet<Teacher> Teachers{get;set;}
     public DbSet<Student> Students{get;set;}
     public DbSet<Subject> Subjects{get;set;}
     public DbSet<Group> Groups{get;set;}
     public DbSet<Grade> Grades{get;set;}
     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
          modelBuilder.Entity<Group>(builder =>
        {
             builder.HasIndex(a=>a.Name).IsUnique();
        });
           modelBuilder.Entity<Student>(builder =>
        {
             builder.HasIndex(a=>a.Phone).IsUnique();
        });
  modelBuilder.Entity<Teacher>(builder =>
        {
             builder.HasIndex(a=>a.Phone).IsUnique();
        });
     }

}