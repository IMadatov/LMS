using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContextOptions;

public class TranslationDbContext : DbContext
{
    public DbSet<Transloco> Translations { get; set; }

    public TranslationDbContext(DbContextOptions<TranslationDbContext> options) : base(options)
    {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transloco>(entity =>
        {
            entity.HasIndex(e => e.Code).IsUnique();
        });
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Transloco).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}
