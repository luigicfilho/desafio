using Kanban.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanban.API.Data;

public class KanbanAppDbContext : DbContext
{
    public DbSet<Card>? Cards { get; set; }
    public DbSet<LoginData>? Logins { get; set; }

    public KanbanAppDbContext(DbContextOptions<KanbanAppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>().ToTable("Card");
        modelBuilder.Entity<LoginData>().ToTable("LoginData");
    }
}
