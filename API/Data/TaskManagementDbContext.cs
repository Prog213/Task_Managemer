using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class TaskManagementDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<AppTask> Tasks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Username).IsUnique();
            entity.HasIndex(u => u.Email).IsUnique();
        });

        modelBuilder.Entity<AppTask>(entity =>
        {
            entity.Property(s => s.Status).HasConversion<string>();
            entity.Property(p => p.Priority).HasConversion<string>();

            entity.HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }
}
