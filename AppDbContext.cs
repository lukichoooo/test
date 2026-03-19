using Microsoft.EntityFrameworkCore;

namespace tmp;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Notebook> Notebooks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notebook>()
            .HasOne(n => n.User)
            .WithOne(u => u.Notebook)
            .HasForeignKey<Notebook>(n => n.UserId);
    }
}
