using Microsoft.EntityFrameworkCore;
using TestNalog.Models;
namespace TestNalog.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Request> Requests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Department).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(30);
        }
        );
        modelBuilder.Entity<Request>(entity =>
        {
            entity.Property(r => r.Title).IsRequired().HasMaxLength(200);
            entity.Property(r => r.Description).IsRequired().HasMaxLength(2000);
            entity.Property(r => r.CreatedDate);
            entity.Property(r => r.Status);
            entity.Property(r => r.Priority);
            entity.HasOne(r => r.Employee)
                .WithMany(e => e.Requests)
                .HasForeignKey(r => r.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        );

    }


}
